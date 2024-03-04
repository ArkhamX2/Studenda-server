using Studenda.Server.Data.Configuration;

namespace Studenda.Server.Data.Factory;

public class DataContextFactory(ContextConfiguration configuration) : IContextFactory<DataContext>
{
    private ContextConfiguration Configuration { get; } = configuration;

    public DataContext CreateDataContext()
    {
        var context = new DataContext(Configuration);

        if (!context.TryInitialize())
        {
            throw new Exception("Failed to initialize data context!");
        }

        return context;
    }
}