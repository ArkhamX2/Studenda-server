using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Data.Util;

namespace Studenda.Core.Model;

/// <summary>
///     Модель стандартного объекта с соответствующей
///     таблицей в базе данных.
/// </summary>
public abstract class Entity
{
    /// <summary>
    ///     Вычислить массив байтов хеш-суммы.
    /// </summary>
    /// <param name="entity">Модель стандартного объекта.</param>
    /// <returns>Массив байтов.</returns>
    private static IEnumerable<byte> ComputeDataHash(Entity entity)
    {
        var json = DataSerializer.Serialize(entity);
        var bytes = Encoding.UTF8.GetBytes(json);

        return MD5.HashData(bytes);
    }

    /*                   __ _                       _   _
     *   ___ ___  _ __  / _(_) __ _ _   _ _ __ __ _| |_(_) ___  _ __
     *  / __/ _ \| '_ \| |_| |/ _` | | | | '__/ _` | __| |/ _ \| '_ \
     * | (_| (_) | | | |  _| | (_| | |_| | | | (_| | |_| | (_) | | | |
     *  \___\___/|_| |_|_| |_|\__, |\__,_|_|  \__,_|\__|_|\___/|_| |_|
     *                        |___/
     * Константы, задающие базовые конфигурации полей
     * и ограничения модели.
     */

    #region Configuration

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="CreatedAt" />.
    /// </summary>
    private const bool IsCreatedAtRequired = false;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="UpdatedAt" />.
    /// </summary>
    private const bool IsUpdatedAtRequired = false;

    /// <summary>
    ///     Конфигурация модели <see cref="Entity" />.
    ///     Используется для дополнительной настройки,
    ///     включая биндинг полей под данные,
    ///     создание зависимостей и маппинг в базе данных.
    /// </summary>
    /// <typeparam name="T">
    ///     <see cref="Entity" />
    /// </typeparam>
    internal abstract class Configuration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        /// <summary>
        ///     Конструктор.
        /// </summary>
        /// <param name="configuration">Конфигурация базы данных.</param>
        protected Configuration(ContextConfiguration configuration)
        {
            ContextConfiguration = configuration;
        }

        /// <summary>
        ///     Конфигурация базы данных.
        /// </summary>
        private ContextConfiguration ContextConfiguration { get; }

        /// <summary>
        ///     Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(entity => entity.CreatedAt)
                .HasColumnType(ContextConfiguration.DateTimeType)
                .HasDefaultValueSql(ContextConfiguration.DateTimeValueCurrent)
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();

            builder.Property(entity => entity.UpdatedAt)
                .HasColumnType(ContextConfiguration.DateTimeType)
                .IsRequired(IsUpdatedAtRequired);
        }
    }

    #endregion

    /*             _   _ _
     *   ___ _ __ | |_(_) |_ _   _
     *  / _ \ '_ \| __| | __| | | |
     * |  __/ | | | |_| | |_| |_| |
     *  \___|_| |_|\__|_|\__|\__, |
     *                       |___/
     * Поля данных, соответствующие таковым в таблице
     * модели в базе данных.
     */

    #region Entity

    /// <summary>
    ///     Дата создания.
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    ///     Дата обновления.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    #endregion

    /// <summary>
    ///     Вычислить и получить массив байтов хеш-суммы.
    /// </summary>
    /// <returns>Массив байтов хеш-суммы.</returns>
    public IEnumerable<byte> GetDataHash() => ComputeDataHash(this);

    /// <summary>
    ///     Сравнить хеш-сумму модели с указанным массивом байтов.
    /// </summary>
    /// <param name="dataHash">Массив байтов хеш-суммы.</param>
    /// <returns>Статус сравнения.</returns>
    public bool CompareWith(IEnumerable<byte> dataHash) => dataHash.SequenceEqual(GetDataHash());

    /// <summary>
    ///     Сравнить хеш-суммы с указанной моделью.
    /// </summary>
    /// <param name="entity">Модель стандартного объекта.</param>
    /// <returns>Статус сравнения.</returns>
    public bool CompareWith(Entity entity) => CompareWith(ComputeDataHash(entity));
}