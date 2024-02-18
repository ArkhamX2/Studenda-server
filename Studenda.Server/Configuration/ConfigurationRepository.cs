using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Studenda.Server.Data.Configuration;

namespace Studenda.Server.Configuration;

public class ConfigurationRepository(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    private static string HandleStringValue(string? value, string exceptionMessage)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new Exception(exceptionMessage);
        }

        return value;
    }

    private static int HandleIntValue(int value, string exceptionMessage)
    {
        if (value <= 0)
        {
            throw new Exception(exceptionMessage);
        }

        return value;
    }

    private string GetDefaultConnectionString()
    {
        var connectionString = Configuration.GetConnectionString("Default");

        return HandleStringValue(connectionString, "Default connection string is null or empty!");
    }

    public ContextConfiguration GetContextConfiguration(bool isDebugMode)
    {
        var connectionString = GetDefaultConnectionString();
        var connectionType = Configuration
            .GetSection("Data")
            .GetValue<string>("ConnectionType");

        HandleStringValue(connectionType, "Connection type is null or empty!");

        return connectionType switch
        {
            "Sqlite" => new SqliteConfiguration(connectionString, isDebugMode),
            "Mysql" => new MysqlConfiguration(connectionString, ServerVersion.AutoDetect(connectionString),
                isDebugMode),
            _ => throw new Exception("Unknown connection type!")
        };
    }

    private string GetTokenIssuer()
    {
        var issuer = Configuration
            .GetSection("Token")
            .GetValue<string>("Issuer");

        return HandleStringValue(issuer, "Token issuer is null or empty!");
    }

    private string GetTokenAudience()
    {
        var audience = Configuration
            .GetSection("Token")
            .GetValue<string>("Audience");

        return HandleStringValue(audience, "Token audience is null or empty!");
    }

    private string GetTokenKey()
    {
        var key = Configuration
            .GetSection("Token")
            .GetValue<string>("Key");

        return HandleStringValue(key, "Token key is null or empty!");
    }

    private int GetTokenClockSkew()
    {
        var skew = Configuration
            .GetSection("Token")
            .GetValue<int>("ClockSkew");

        return HandleIntValue(skew, "Token clock skew is invalid!");
    }

    public TokenValidationParameters GetTokenValidationParameters()
    {
        var key = Encoding.UTF8.GetBytes(GetTokenKey());
        var clockSkew = GetTokenClockSkew();

        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = GetTokenIssuer(),
            ValidAudience = GetTokenAudience(),
            ClockSkew = TimeSpan.FromMinutes(clockSkew),
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    }
}