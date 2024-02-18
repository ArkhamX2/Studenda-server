using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model;
using Studenda.Core.Model.Common;
using Studenda.Core.Model.Journal;
using Studenda.Core.Model.Journal.Management;
using Studenda.Core.Model.Schedule;
using Studenda.Core.Model.Schedule.Management;
using Studenda.Core.Model.Security;
using Studenda.Core.Model.Security.Link;
using Studenda.Core.Model.Security.Management;
using Task = Studenda.Core.Model.Journal.Task;

namespace Studenda.Core.Data;

/// <summary>
///     Сессия работы с базой данных.
///     Памятка для работы с кешем:
///     - context.Add() для запроса INSERT.
///     Объекты вставляются со статусом Added.
///     При коммите изменений произойдет попытка вставки.
///     - context.Update() для UPDATE.
///     Объекты вставляются со статусом Modified.
///     При коммите изменений произойдет попытка обновления.
///     - context.Attach() для вставки в кеш.
///     Объекты вставляются со статусом Unchanged.
///     При коммите изменений ничего не произойдет.
/// </summary>
public class DataContext : DbContext
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="configuration">Конфигурация базы данных.</param>
    public DataContext(ContextConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    ///     Конфигурация базы данных.
    /// </summary>
    private ContextConfiguration Configuration { get; }

    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Group> Groups => Set<Group>();

    public DbSet<SubjectPosition> SubjectPositions => Set<SubjectPosition>();
    public DbSet<DayPosition> DayPositions => Set<DayPosition>();
    public DbSet<WeekType> WeekTypes => Set<WeekType>();
    public DbSet<Discipline> Disciplines => Set<Discipline>();
    public DbSet<SubjectType> SubjectTypes => Set<SubjectType>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<SubjectChange> SubjectChanges => Set<SubjectChange>();

    public DbSet<Task> Tasks => Set<Task>();
    public DbSet<Absence> Absences => Set<Absence>();
    public DbSet<Assessment> Assessments => Set<Assessment>();
    public DbSet<AssessmentType> AssessmentTypes => Set<AssessmentType>();

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermissionLink> RolePermissionLinks => Set<RolePermissionLink>();

    /// <summary>
    ///     Попытаться инициализировать сессию.
    ///     Используется для проверки подключения
    ///     и инициализации структуры таблиц.
    /// </summary>
    /// <returns>Статус успешности инициализации.</returns>
    public bool TryInitialize()
    {
        var canConnect = Database.CanConnect();
        var isCreated = Database.EnsureCreated();

        return canConnect || isCreated;
    }

    /// <summary>
    ///     Попытаться асинхронно инициализировать сессию.
    ///     Используется для проверки подключения
    ///     и инициализации структуры таблиц.
    /// </summary>
    /// <returns>Статус успешности инициализации.</returns>
    public async Task<bool> TryInitializeAsync()
    {
        var canConnect = await Database.CanConnectAsync();
        var isCreated = await Database.EnsureCreatedAsync();

        return canConnect || isCreated;
    }

    /// <summary>
    ///     Обработать настройку сессии.
    /// </summary>
    /// <param name="optionsBuilder">Набор интерфейсов настройки сессии.</param>
    /// <exception cref="Exception">При ошибке подключения.</exception>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Configuration.ConfigureContext(optionsBuilder);

        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    ///     Обработать инициализацию модели.
    ///     Используется для дополнительной настройки модели.
    /// </summary>
    /// <param name="modelBuilder">Набор интерфейсов настройки модели.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Общее.
        modelBuilder.ApplyConfiguration(new Department.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new Course.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new Group.Configuration(Configuration));

        // Расписание.
        modelBuilder.ApplyConfiguration(new SubjectPosition.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new DayPosition.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new WeekType.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new Discipline.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new SubjectType.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new Subject.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new SubjectChange.Configuration(Configuration));

        // Журнал.
        modelBuilder.ApplyConfiguration(new Task.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new Absence.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new Assessment.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new AssessmentType.Configuration(Configuration));

        // Безопасность.
        modelBuilder.ApplyConfiguration(new User.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new Role.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new Permission.Configuration(Configuration));
        modelBuilder.ApplyConfiguration(new RolePermissionLink.Configuration(Configuration));

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    ///     Сохранить все изменения сессии в базу данных.
    ///     Используется для обновления метаданных модели.
    /// </summary>
    /// <returns>Количество затронутых записей.</returns>
    public override int SaveChanges()
    {
        UpdateTrackedEntityMetadata();

        return base.SaveChanges();
    }

    /// <summary>
    ///     Асинхронно сохранить все изменения сессии в базу данных.
    ///     Используется для обновления метаданных модели.
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">Флаг принятия всех изменений при успехе операции.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Таск, представляющий операцию асинхронного сохранения с количеством затронутых записей.</returns>
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        UpdateTrackedEntityMetadata();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    ///     Обновить метаданные всех модифицироанных моделей
    ///     в кеше сессии.
    ///     Такой подход накладывает дополнительные ограничения
    ///     при работе с сессиями. Необходимо учитывать, что
    ///     для обновления записей нужно сперва загрузить эти
    ///     записи в кеш сессии, чтобы трекер корректно
    ///     зафиксировал изменения.
    /// </summary>
    private void UpdateTrackedEntityMetadata()
    {
        var entries = ChangeTracker.Entries().Where(IsModifiedEntity);

        foreach (var entry in entries)
        {
            if (entry.Entity is not Entity entity) continue;

            // Текущая дата и время на устройстве.
            // Нельзя допустить, чтобы эти данные передавались во внешние хранилища.
            entity.UpdatedAt = DateTime.Now.ToUniversalTime();
        }
    }

    /// <summary>
    ///     Определить, является ли вхождение трекера обновленной моделью.
    /// </summary>
    /// <param name="entry">Вхождение из трекера изменений.</param>
    /// <returns>Статус проверки.</returns>
    private static bool IsModifiedEntity(EntityEntry entry)
    {
        return entry is
        {
            Entity: Entity, State: EntityState.Modified
        };
    }
}