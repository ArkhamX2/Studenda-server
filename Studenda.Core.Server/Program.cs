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

var applicationBuilder = WebApplication.CreateBuilder(args);
var dataConfiguration = new SqliteConfiguration("Data Source=000_debug_data_storage.db", isDebugMode);
var identityConfiguration = new SqliteConfiguration("Data Source=001_debug_identity_storage.db", isDebugMode);

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
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // указывает, будет ли валидироваться издатель при валидации токена
        ValidateIssuer = true,
        // строка, представляющая издателя
        ValidIssuer = JwtManager.Issuer,
        // будет ли валидироваться потребитель токена
        ValidateAudience = true,
        // установка потребителя токена
        ValidAudience = JwtManager.Audience,
        // будет ли валидироваться время существования
        ValidateLifetime = true,
        // установка ключа безопасности
        IssuerSigningKey = JwtManager.GetSymmetricSecurityKey(),
        // валидация ключа безопасности
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.FromMinutes(2)
    };
});

var application = applicationBuilder.Build();
application.UseMiddleware<ExceptionHandler>();
application.MapControllers();
application.Run();