using Studenda.Server.Data.Configuration;

namespace Studenda.Server.Data.Factory;

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