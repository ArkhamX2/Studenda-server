using Studenda.Core.Data.Configuration;
using Studenda.Core.Server.Common.Data.Factory;

namespace Studenda.Core.Server.Security.Data.Factory;

public class IdentityContextFactory(ContextConfiguration configuration) : IContextFactory<IdentityContext>
{
    private ContextConfiguration Configuration { get; } = configuration;

    public IdentityContext CreateDataContext()
    {
        var context = new IdentityContext(Configuration);

        if (!context.TryInitialize())
        {
            throw new Exception("Failed to initialize identity context!");
        }

        return context;
    }
}