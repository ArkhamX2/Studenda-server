using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Studenda.Server.Data;
using Studenda.Server.Data.Factory;
using Studenda.Server.Middleware;
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
        services.AddScoped<MarkService>();
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
    /// <param name="configuration">Менеджер конфигурации.</param>
    private static void RegisterDataSources(IServiceCollection services, ConfigurationManager configuration)
    {
        var dataConfiguration = configuration.DataConfiguration.GetDefaultContextConfiguration(IsDebugMode);
        var identityDataConfiguration = configuration.DataConfiguration.GetIdentityContextConfiguration(IsDebugMode);

        services.AddSingleton<IContextFactory<DataContext>>(new DataContextFactory(dataConfiguration));
        services.AddSingleton<IContextFactory<IdentityContext>>(new IdentityContextFactory(identityDataConfiguration));

        services.AddScoped(provider =>
        {
            var factory = provider.GetService<IContextFactory<DataContext>>();

            return factory!.CreateDataContext();
        });
        services.AddScoped(provider =>
        {
            var factory = provider.GetService<IContextFactory<IdentityContext>>();

            return factory!.CreateDataContext();
        });
    }

    /// <summary>
    ///     Зарегистрировать сервисы идентификации.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Менеджер конфигурации.</param>
    private static void RegisterIdentityServices(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddUserManager<UserManager<IdentityUser>>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddSignInManager<SignInManager<IdentityUser>>();

        services.Configure<IdentityOptions>(options => configuration.IdentityConfiguration.GetOptions());
    }

    /// <summary>
    ///     Зарегистрировать сервисы безопасности.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Менеджер конфигурации.</param>
    private static void RegisterSecurityServices(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuthorization();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.IncludeErrorDetails = true;
            options.TokenValidationParameters = configuration.TokenConfiguration.GetValidationParameters();
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
    ///     Точка входа в приложение.
    /// </summary>
    /// <param name="args">Аргументы из командной строки.</param>
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var configuration = new ConfigurationManager(builder.Configuration);

        RegisterCoreServices(services);
        RegisterDataSources(services, configuration);
        RegisterIdentityServices(services, configuration);
        RegisterSecurityServices(services, configuration);

        var application = builder.Build();

        application.UseMiddleware<ExceptionHandler>();
        application.UseAuthentication();
        application.UseAuthorization();
        application.MapControllers();
        application.UseCors();
        application.Run();
    }
}