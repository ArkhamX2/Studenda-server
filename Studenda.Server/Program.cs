using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Studenda.Server.Configuration;
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
var configuration = new ConfigurationRepository(applicationBuilder.Configuration);
var dataConfiguration = configuration.GetContextConfiguration(isDebugMode);

// Сервисы.

var serviceCollection = applicationBuilder.Services;

serviceCollection.AddSingleton<IContextFactory<DataContext>>(new DataContextFactory(dataConfiguration));
serviceCollection.AddScoped<DataContext>(provider =>
{
    var factory = provider.GetService<IContextFactory<DataContext>>();

    return factory!.CreateDataContext();
});

serviceCollection.AddScoped<TokenService>();
serviceCollection.AddScoped<DataEntityService>();
serviceCollection.AddScoped<SubjectService>();
serviceCollection.AddScoped<WeekTypeService>();
serviceCollection.AddTransient<ConfigurationRepository>();
serviceCollection.AddControllers();

serviceCollection.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddUserManager<UserManager<User>>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddSignInManager<SignInManager<User>>();

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