using Microsoft.EntityFrameworkCore;

namespace Studenda.Core.Server.Common.Data.Factory;

public interface IContextFactory<out TContext> where TContext : DbContext
{
    TContext CreateDataContext();
}