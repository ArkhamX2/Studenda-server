using Microsoft.EntityFrameworkCore;

namespace Studenda.Server.Data.Factory;

public interface IContextFactory<out TContext> where TContext : DbContext
{
    TContext CreateDataContext();
}