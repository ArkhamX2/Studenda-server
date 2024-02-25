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

    private string GetConnectionType()
    {
        var result = Configuration
            .GetSection("Data")
            .GetValue<string>("ConnectionType");

        return HandleStringValue(result, "Connection type is null or empty!");
    }

    public ContextConfiguration GetDefaultContextConfiguration(bool isDebugMode)
    {
        var connectionString = Configuration.GetConnectionString("Default");

        HandleStringValue(connectionString, "Default connection string is null or empty!");

        return GetConnectionType().ToLower() switch
        {
            "sqlite" => new SqliteConfiguration(connectionString!, isDebugMode),
            "mysql" => new MysqlConfiguration(connectionString!, ServerVersion.AutoDetect(connectionString),
                isDebugMode),
            _ => throw new Exception("Unknown connection type!")
        };
    }

    public ContextConfiguration GetIdentityContextConfiguration(bool isDebugMode)
    {
        var connectionString = Configuration.GetConnectionString("Identity");

        HandleStringValue(connectionString, "Identity connection string is null or empty!");

        return GetConnectionType().ToLower() switch
        {
            "sqlite" => new SqliteConfiguration(connectionString!, isDebugMode),
            "mysql" => new MysqlConfiguration(connectionString!, ServerVersion.AutoDetect(connectionString),
                isDebugMode),
            _ => throw new Exception("Unknown connection type!")
        };
    }

    public string GetTokenIssuer()
    {
        var result = Configuration
            .GetSection("Token")
            .GetValue<string>("Issuer");

        return HandleStringValue(result, "Token issuer is null or empty!");
    }

    public string GetTokenAudience()
    {
        var result = Configuration
            .GetSection("Token")
            .GetValue<string>("Audience");

        return HandleStringValue(result, "Token audience is null or empty!");
    }

    public string GetTokenClaimNameSub()
    {
        var result = Configuration
            .GetSection("Token")
            .GetValue<string>("ClaimNameSub");

        return HandleStringValue(result, "Token claim name sub is null or empty!");
    }

    private string GetTokenKey()
    {
        var result = Configuration
            .GetSection("Token")
            .GetValue<string>("Key");

        return HandleStringValue(result, "Token key is null or empty!");
    }

    private int GetTokenClockSkew()
    {
        var result = Configuration
            .GetSection("Token")
            .GetValue<int>("ClockSkewMinutes");

        return HandleIntValue(result, "Token clock skew is invalid!");
    }

    public int GetTokenLifetimeMinutes()
    {
        var result = Configuration
            .GetSection("Token")
            .GetValue<int>("LifetimeMinutes");

        return HandleIntValue(result, "Token lifetime is invalid!");
    }

    public SymmetricSecurityKey GetTokenSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetTokenKey()));
    }

    public TokenValidationParameters GetTokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = GetTokenIssuer(),
            ValidAudience = GetTokenAudience(),
            ClockSkew = TimeSpan.FromMinutes(GetTokenClockSkew()),
            IssuerSigningKey = GetTokenSecurityKey()
        };
    }
}