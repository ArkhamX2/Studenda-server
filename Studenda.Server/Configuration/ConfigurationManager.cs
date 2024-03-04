using Studenda.Server.Configuration.Repository;

namespace Studenda.Server.Configuration;

public class ConfigurationManager(IConfiguration configuration)
{
    public DataConfiguration DataConfiguration { get; } = new DataConfiguration(configuration);
    public IdentityConfiguration IdentityConfiguration { get; } = new IdentityConfiguration(configuration);
    public TokenConfiguration TokenConfiguration { get; } = new TokenConfiguration(configuration);
}