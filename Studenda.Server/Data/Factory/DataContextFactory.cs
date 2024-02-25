using Studenda.Server.Data.Configuration;
using Studenda.Server.Model.Security.Management;

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

        // TODO: Использовать миграции? Сценарии инициализации?
        if (context.Roles.Any(role => role.Name == "admin")) return context;

        var role = new Role { Name = "admin" };

        context.Roles.Add(role);
        context.SaveChanges();

        return context;
    }
}