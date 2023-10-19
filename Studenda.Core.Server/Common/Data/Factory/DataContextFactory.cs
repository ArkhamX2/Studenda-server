using Studenda.Core.Data;
using Studenda.Core.Data.Configuration;

namespace Studenda.Core.Server.Common.Data.Factory;

public class DataContextFactory : IContextFactory<DataContext>
{
    public DataContextFactory(ContextConfiguration configuration)
    {
        Configuration = configuration;
    }

    private ContextConfiguration Configuration { get; }

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