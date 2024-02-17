using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Studenda.Server.Data;
using Studenda.Server.Data.Configuration;
using Studenda.Server.Data.Factory;
using Studenda.Server.Middleware;
using Studenda.Server.Model.Security;
using Studenda.Server.Service;
using Studenda.Server.Service.Schedule;
using Studenda.Server.Service.Security;

#if DEBUG
const bool isDebugMode = true;
#else
const bool isDebugMode = false;
#endif

var applicationBuilder = WebApplication.CreateBuilder(args);
var configuration = applicationBuilder.Configuration;

// База данных.

var connectionString = configuration.GetConnectionString("Default");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Default connection string is null or empty!");
}

// TODO: Конфигурация контекстов на основе конфигурации приложения.
var dataConfiguration = new MysqlConfiguration(connectionString, ServerVersion.AutoDetect(connectionString), isDebugMode);

// Сервисы.

var serviceCollection = applicationBuilder.Services;

serviceCollection.AddSingleton<IContextFactory<DataContext>>(new DataContextFactory(dataConfiguration));

serviceCollection.AddScoped<DataContext>(provider =>
{
    var factory = provider.GetService<IContextFactory<DataContext>>();

    return factory!.CreateDataContext();
});

serviceCollection.AddScoped<TokenService>();
serviceCollection.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddUserManager<UserManager<User>>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddSignInManager<SignInManager<User>>();

serviceCollection.AddScoped<DataEntityService>();
serviceCollection.AddScoped<SubjectService>();
serviceCollection.AddScoped<WeekTypeService>();
serviceCollection.AddControllers();
serviceCollection.AddAuthorization();
serviceCollection.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
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