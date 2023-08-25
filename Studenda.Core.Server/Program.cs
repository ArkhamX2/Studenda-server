using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Studenda.Core.Data;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Server.Utils;
using Studenda.Core.Server.Utils.Token;


#if DEBUG
const bool isDebugMode = true;
#else
const bool isDebugMode = false;
#endif
var builder = WebApplication.CreateBuilder(args);
//добавляет контроллеры
builder.Services.AddControllers();
//добавляет в  builder конфигурацию для базы данных(необходимо для  sqlite)
builder.Services.AddSingleton<ContextConfiguration>(_ => new SqliteConfiguration("Data Source=000_debug_storage.db", isDebugMode));
//добавляет базу данных
builder.Services.AddDbContext<DataContext>(o => o.UseSqlite());

builder.Services.AddScoped<ITokenService, TokenService>();
//добавляет корс
builder.Services.AddCors(c => c.AddPolicy("cors", opt =>
{
    opt.AllowAnyHeader();
    opt.AllowCredentials();
    opt.AllowAnyMethod();
    opt.WithOrigins(builder.Configuration.GetSection("Cors:Urls").Get<string[]>()!);
}));
builder.Services.AddIdentity<Person, IdentityRole<long>>()
                .AddEntityFrameworkStores<DataContext>()
                .AddUserManager<UserManager<Person>>()
                .AddSignInManager<SignInManager<Person>>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOptions.ISSUER,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.FromMinutes(2),
        };
    });


var app = builder.Build();
app.UseCors("cors");
app.MapControllers();
app.Run();