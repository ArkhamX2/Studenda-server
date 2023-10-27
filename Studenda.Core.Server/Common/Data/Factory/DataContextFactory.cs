using Studenda.Core.Data;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Security.Management;

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

        // TODO: Использовать миграции? Сценарии инициализации?
        if (context.Roles.Any(role => role.Name == "admin")) return context;

        var role = new Role { Name = "admin" };

        context.Roles.Add(role);
        context.SaveChanges();

        return context;
    }
}