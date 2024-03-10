using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Studenda.Server.Configuration.Repository;
using Studenda.Server.Data;
using Studenda.Server.Data.Initialization;
using Studenda.Server.Middleware;
using Studenda.Server.Middleware.Security;
using Studenda.Server.Middleware.Security.Requirement;
using Studenda.Server.Service;
using Studenda.Server.Service.Journal;
using Studenda.Server.Service.Schedule;
using Studenda.Server.Service.Security;
using ConfigurationManager = Studenda.Server.Configuration.ConfigurationManager;

/// <summary>
///     Класс для запуска приложения.
/// </summary>
internal class Program
{
#if DEBUG
    private const bool IsDebugMode = true;
#else
    private const bool IsDebugMode = false;
#endif

    /// <summary>
    ///     Точка входа в приложение.
    /// </summary>
    /// <param name="args">Аргументы из командной строки.</param>
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = new ConfigurationManager(builder.Configuration);

        RegisterCoreServices(builder.Services);
        RegisterDataSources(builder.Services, configuration.DataConfiguration);
        RegisterIdentityServices(builder.Services, configuration.IdentityConfiguration);
        RegisterAuthorizationServices(builder.Services);
        RegisterAuthenticationServices(builder.Services, configuration.TokenConfiguration);
        RegisterCorsServices(builder.Services);

        var application = builder.Build();

        application.UseMiddleware<ExceptionHandler>();
        application.UseAuthentication();
        application.UseAuthorization();
        application.MapControllers();
        application.UseCors();

        InitializeDataSources(application);

        application.Run();
    }

    /// <summary>
    ///     Зарегистрировать основные сервисы и контроллеры.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    private static void RegisterCoreServices(IServiceCollection services)
    {
        services.AddScoped<TokenService>();
        services.AddScoped<AccountService>();
        services.AddScoped<DataEntityService>();
        services.AddScoped<SubjectService>();
        services.AddScoped<WeekTypeService>();
        services.AddScoped<TaskService>();
        services.AddScoped<AbsenceService>();
        services.AddScoped<SessionService>();
        services.AddTransient<ConfigurationManager>();
        services.AddControllers();
    }

    /// <summary>
    ///     Зарегистрировать источники данных.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурации данных.</param>
    private static void RegisterDataSources(IServiceCollection services, DataConfiguration configuration)
    {
        var dataConfiguration = configuration.GetDefaultContextConfiguration(IsDebugMode);
        var identityConfiguration = configuration.GetIdentityContextConfiguration(IsDebugMode);

        services.AddScoped(provider => new DataContext(dataConfiguration));
        services.AddScoped(provider => new IdentityContext(identityConfiguration));

        services.AddScoped<DataInitializationScript>();
        services.AddScoped<IdentityInitializationScript>();
    }

    /// <summary>
    ///     Зарегистрировать сервисы идентификации.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурации модуля идентификации.</param>
    private static void RegisterIdentityServices(IServiceCollection services, IdentityConfiguration configuration)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddUserManager<UserManager<IdentityUser>>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddSignInManager<SignInManager<IdentityUser>>();

        services.Configure<IdentityOptions>(options => configuration.GetOptions());
    }

    /// <summary>
    ///     Зарегистрировать сервисы авторизации.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    private static void RegisterAuthorizationServices(IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(
                StudentRoleAuthorizationRequirement.AuthorizationPolicyCode,
                policy => policy.Requirements.Add(new StudentRoleAuthorizationRequirement()))
            .AddPolicy(
                LeaderRoleAuthorizationRequirement.AuthorizationPolicyCode,
                policy => policy.Requirements.Add(new LeaderRoleAuthorizationRequirement()))
            .AddPolicy(
                TeacherRoleAuthorizationRequirement.AuthorizationPolicyCode,
                policy => policy.Requirements.Add(new TeacherRoleAuthorizationRequirement()))
            .AddPolicy(
                AdminRoleAuthorizationRequirement.AuthorizationPolicyCode,
                policy => policy.Requirements.Add(new AdminRoleAuthorizationRequirement()));

        services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler<StudentRoleAuthorizationRequirement>>();
        services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler<LeaderRoleAuthorizationRequirement>>();
        services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler<TeacherRoleAuthorizationRequirement>>();
        services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler<AdminRoleAuthorizationRequirement>>();
    }

    /// <summary>
    ///     Зарегистрировать сервисы аутентификации.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация токенов.</param>
    private static void RegisterAuthenticationServices(IServiceCollection services, TokenConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.IncludeErrorDetails = true;
            options.TokenValidationParameters = configuration.GetValidationParameters();
        });

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });
    }

    /// <summary>
    ///     Зарегистрировать сервисы межсайтовой аутентификации.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    private static void RegisterCorsServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });
    }

    /// <summary>
    ///     Инициализировать сессии данных.
    /// </summary>
    /// <param name="application">Приложение.</param>
    private static async void InitializeDataSources(WebApplication application)
    {
        using var scope = application.Services.CreateScope();

        await scope.ServiceProvider.GetRequiredService<DataInitializationScript>().Run();
        await scope.ServiceProvider.GetRequiredService<IdentityInitializationScript>().Run();
    }
}