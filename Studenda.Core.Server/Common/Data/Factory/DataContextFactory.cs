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
        return new DataContext(Configuration);
    }
}