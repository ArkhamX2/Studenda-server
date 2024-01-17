using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Studenda.Core.Data;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Server.Common.Data.Factory;
using Studenda.Core.Server.Common.Middleware;
using Studenda.Core.Server.Common.Service;
using Studenda.Core.Server.Schedule.Service;
using Studenda.Core.Server.Security.Data;
using Studenda.Core.Server.Security.Data.Factory;
using Studenda.Core.Server.Security.Service;
using Studenda.Core.Server.Security.Service.Token;

#if DEBUG
const bool isDebugMode = true;
#else
const bool isDebugMode = false;
#endif

var applicationBuilder = WebApplication.CreateBuilder(args);
var configuration = applicationBuilder.Configuration;

// Базы данных.

var defaultConnectionString = configuration.GetConnectionString("Default");
var identityConnectionString = configuration.GetConnectionString("Identity");

if (string.IsNullOrEmpty(defaultConnectionString))
{
    throw new Exception("Default connection string is null or empty!");
}

if (string.IsNullOrEmpty(identityConnectionString))
{
    throw new Exception("Identity connection string is null or empty!");
}

// TODO: Конфигурация контекстов на основе конфигурации приложения.
var dataConfiguration = new MysqlConfiguration(defaultConnectionString, ServerVersion.AutoDetect(defaultConnectionString), isDebugMode);
var identityConfiguration = new MysqlConfiguration(identityConnectionString, ServerVersion.AutoDetect(identityConnectionString), isDebugMode);

// Сервисы.

var serviceCollection = applicationBuilder.Services;

serviceCollection.AddSingleton<IContextFactory<DataContext>>(new DataContextFactory(dataConfiguration));
serviceCollection.AddSingleton<IContextFactory<IdentityContext>>(new IdentityContextFactory(identityConfiguration));

serviceCollection.AddScoped<DataContext>(provider =>
{
    var factory = provider.GetService<IContextFactory<DataContext>>();

    return factory!.CreateDataContext();
});
serviceCollection.AddScoped<IdentityContext>(provider =>
{
    var factory = provider.GetService<IContextFactory<IdentityContext>>();

    return factory!.CreateDataContext();
});

serviceCollection.AddScoped<ITokenService, TokenService>();
serviceCollection.AddIdentity<Account, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddUserManager<UserManager<Account>>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddSignInManager<SignInManager<Account>>();

serviceCollection.AddScoped<DataEntityService>();
serviceCollection.AddScoped<SubjectService>();
serviceCollection.AddControllers();
serviceCollection.AddAuthorization();
serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
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
serviceCollection.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
});

serviceCollection.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Приложение.

var application = applicationBuilder.Build();

application.UseMiddleware<ExceptionHandler>();
application.UseAuthentication();
application.UseAuthorization();
application.MapControllers();
application.UseCors();
application.Run();