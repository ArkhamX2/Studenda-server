using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Studenda.Core.Data;
using Studenda.Core.Data.Configuration;

#if DEBUG
const bool isDebugMode = true;
#else
const bool isDebugMode = false;
#endif
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<ContextConfiguration>(_ => new SqliteConfiguration("Data Source=000_debug_storage.db", isDebugMode));
builder.Services.AddDbContext<DataContext>(o=>o.UseSqlite());

var app = builder.Build();
app.MapControllers();
app.Run();