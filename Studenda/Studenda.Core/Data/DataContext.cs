using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model;
using Studenda.Core.Model.Account;
using Studenda.Core.Model.Common;
using Studenda.Core.Model.Link;

namespace Studenda.Core.Data;

/// <summary>
/// Сессия работы с базой данных.
/// 
/// TODO: Создать класс управления контекстами и их конфигурациями.
///
/// Памятка для работы с кешем:
/// - context.Add() для запроса INSERT.
///   Объекты вставляются со статусом Added.
///   При коммите изменений произойдет попытка вставки.
/// - context.Update() для UPDATE.
///   Объекты вставляются со статусом Modified.
///   При коммите изменений произойдет попытка обновления.
/// - context.Attach() для вставки в кеш.
///   Объекты вставляются со статусом Unchanged.
///   При коммите изменений ничего не произойдет.
/// </summary>
public sealed class DataContext : DbContext
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    public DataContext(ContextConfiguration configuration)
    {
        Configuration = configuration;

        // TODO: Использовать асинхронные запросы.
        if (!Database.CanConnect())
        {
            if (!Database.EnsureCreated())
            {
                throw new Exception("Connection error!");
            }
        }
        else
        {
            Database.EnsureCreated();
        }
    }

    /// <summary>
    /// Конфигурация базы данных.
    /// </summary>
    private ContextConfiguration Configuration { get; }

    /// <summary>
    /// Набор объектов <see cref="User"/>.
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <summary>
    /// Набор объектов <see cref="Role"/>.
    /// </summary>
    public DbSet<Role> Roles => Set<Role>();

    /// <summary>
    /// Набор объектов <see cref="Permission"/>.
    /// </summary>
    public DbSet<Permission> Permissions => Set<Permission>();

    /// <summary>
    /// Набор объектов <see cref="Department"/>.
    /// </summary>
    public DbSet<Department> Departments => Set<Department>();

    /// <summary>
    /// Набор объектов <see cref="Course"/>.
    /// </summary>
    public DbSet<Course> Courses => Set<Course>();

    /// <summary>
    /// Набор объектов <see cref="Group"/>.
    /// </summary>
    public DbSet<Group> Groups => Set<Group>();

    /// <summary>
    /// Набор объектов <see cref="WeekType"/>.
    /// </summary>
    public DbSet<WeekType> WeekTypes => Set<WeekType>();

    /// <summary>
    /// Набор объектов <see cref="UserGroupLink"/>.
    /// </summary>
    public DbSet<UserGroupLink> UserGroupLinks => Set<UserGroupLink>();

    /// <summary>
    /// Набор объектов <see cref="RolePermissionLink"/>.
    /// </summary>
    public DbSet<RolePermissionLink> RolePermissionLinks => Set<RolePermissionLink>();

    /// <summary>
    /// Обработать инициализацию сессии.
    /// Используется для настройки сессии.
    /// </summary>
    /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Configuration.ConfigureContext(optionsBuilder);

        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    /// Обработать инициализацию модели.
    /// Используется для дополнительной настройки модели.
    /// </summary>
    /// <param name="modelBuilder">Набор интерфейсов настройки модели.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Использование Fluent API.
        modelBuilder.ApplyConfiguration(new User.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new Role.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new Permission.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new Department.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new Course.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new Group.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new WeekType.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new UserGroupLink.Configuration());
        modelBuilder.ApplyConfiguration(new RolePermissionLink.Configuration());

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Сохранить все изменения сессии в базу данных.
    /// Используется для обновления метаданных модели.
    /// </summary>
    /// <returns>Количество затронутых записей.</returns>
    public override int SaveChanges()
    {
        UpdateTrackedEntityMetadata();

        return base.SaveChanges();
    }

    /// <summary>
    /// Асинхронно сохранить все изменения сессии в базу данных.
    /// Используется для обновления метаданных модели.
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">Флаг принятия всех изменений при успехе операции.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Таск, представляющий операцию асинхронного сохранения с количеством затронутых записей.</returns>
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateTrackedEntityMetadata();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// Обновить метаданные всех добавленных
    /// и модифицироанных моделей в кеше сессии.
    /// Такой подход накладывает дополнительные ограничения
    /// при работе с сессиями. Необходимо учитывать, что
    /// для обновления записей нужно сперва загрузить эти
    /// записи в кеш сессии, чтобы трекер корректно
    /// зафиксировал изменения.
    /// </summary>
    private void UpdateTrackedEntityMetadata()
    {
        var entries = ChangeTracker.Entries().Where(entry => (entry.Entity is Entity)
            && (entry.State == EntityState.Added || entry.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            if (entry.Entity is not Entity entity)
            {
                continue;
            }
            
            // Обновлен существующий объект.
            if (entry.State == EntityState.Modified)
            {
                // Текущая дата и время на устройстве.
                // Нельзя допустить, чтобы эти данные передавались во внешние хранилища.
                entity.UpdatedAt = DateTime.Now.ToUniversalTime();
            }
        }
    }
}
