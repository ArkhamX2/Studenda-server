using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Studenda.Core.Data;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Server.Common.Data.Factory;
using Studenda.Core.Server.Security.Data;
using Studenda.Core.Server.Security.Data.Factory;
using Studenda.Core.Server.Security.Service;
using Studenda.Core.Server.Security.Service.Token;

#if DEBUG
const bool isDebugMode = true;
#else
const bool isDebugMode = false;
#endif

// TODO: Использовать отдельный класс для работы с конфигурацией.
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .Build();

var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
var identityConnectionString = configuration.GetConnectionString("IdentityConnection");

if (string.IsNullOrEmpty(defaultConnectionString) || string.IsNullOrEmpty(identityConnectionString))
{
    throw new Exception("Connection string is null or empty!");
}

// TODO: Конфигурация контекстов на основе конфигурации приложения.
var dataConfiguration = new SqliteConfiguration(defaultConnectionString, isDebugMode);
var identityConfiguration = new SqliteConfiguration(identityConnectionString, isDebugMode);

var applicationBuilder = WebApplication.CreateBuilder(args);

applicationBuilder.Services.AddSingleton<IContextFactory<DataContext>>(
    new DataContextFactory(dataConfiguration));
applicationBuilder.Services.AddScoped<DataContext>(provider =>
{
    var factory = provider.GetService<IContextFactory<DataContext>>();

    return factory!.CreateDataContext();
});

applicationBuilder.Services.AddSingleton<IContextFactory<IdentityContext>>(
    new IdentityContextFactory(identityConfiguration));

applicationBuilder.Services.AddScoped<IdentityContext>(provider =>
{
    var factory = provider.GetService<IContextFactory<IdentityContext>>();

    return factory!.CreateDataContext();
});

applicationBuilder.Services.AddControllers();

applicationBuilder.Services.AddScoped<ITokenService, TokenService>();
applicationBuilder.Services.AddIdentity<Account, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddUserManager<UserManager<Account>>()
    .AddSignInManager<SignInManager<Account>>();

applicationBuilder.Services.AddAuthorization();
applicationBuilder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    // TODO: Вынести в отдельный класс ближе к конфигурациям.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = JwtManager.Issuer,
        ValidAudience = JwtManager.Audience,
        ClockSkew = TimeSpan.FromMinutes(2),
        IssuerSigningKey = JwtManager.GetSymmetricSecurityKey()
    };
});

var application = applicationBuilder.Build();

application.UseMiddleware<ExceptionHandler>();
application.UseAuthentication();
application.UseAuthorization();
application.MapControllers();
application.Run();