using Studenda.Core.Data.Configuration;
using Studenda.Core.Server.Common.Data.Factory;

namespace Studenda.Core.Server.Security.Data.Factory;

public class IdentityContextFactory : IContextFactory<IdentityContext>
{
    public IdentityContextFactory(ContextConfiguration configuration)
    {
        Configuration = configuration;
    }

    private ContextConfiguration Configuration { get; }

    public IdentityContext CreateDataContext()
    {
        return new IdentityContext(Configuration);
    }
}