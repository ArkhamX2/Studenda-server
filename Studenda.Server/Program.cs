using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Studenda.Server.Configuration;
using Studenda.Server.Data;
using Studenda.Server.Data.Factory;
using Studenda.Server.Middleware;
using Studenda.Server.Service;
using Studenda.Server.Service.Journal;
using Studenda.Server.Service.Schedule;
using Studenda.Server.Service.Security;

#if DEBUG
const bool isDebugMode = true;
#else
const bool isDebugMode = false;
#endif

var applicationBuilder = WebApplication.CreateBuilder(args);
var serviceCollection = applicationBuilder.Services;
var configuration = new ConfigurationRepository(applicationBuilder.Configuration);

// Базы данных.

var dataConfiguration = configuration.GetDefaultContextConfiguration(isDebugMode);
var identityConfiguration = configuration.GetIdentityContextConfiguration(isDebugMode);

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

// Сервисы.

serviceCollection.AddScoped<TokenService>();
serviceCollection.AddScoped<UserService>();
serviceCollection.AddScoped<DataEntityService>();
serviceCollection.AddScoped<SubjectService>();
serviceCollection.AddScoped<WeekTypeService>();
serviceCollection.AddScoped<MarkService>();
serviceCollection.AddScoped<TaskService>();
serviceCollection.AddScoped<AbsenceService>();
serviceCollection.AddTransient<ConfigurationRepository>();
serviceCollection.AddControllers();

serviceCollection.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddUserManager<UserManager<IdentityUser>>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddSignInManager<SignInManager<IdentityUser>>();

// TODO: Использовать конфигурации?
serviceCollection.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});

serviceCollection.AddAuthorization();
serviceCollection.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.IncludeErrorDetails = true;
    options.TokenValidationParameters = configuration.GetTokenValidationParameters();
});

serviceCollection.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
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