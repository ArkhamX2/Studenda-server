using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Studenda.Core.Data;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Account;
using Studenda.Core.Server.Data;
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
builder.Services.AddDbContext<ServerDataContext>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddIdentity<Account, IdentityRole<long>>()
                .AddEntityFrameworkStores<ServerDataContext>()
                .AddUserManager<UserManager<Account>>()
                .AddSignInManager<SignInManager<Account>>();
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
app.MapControllers();
app.Run();