using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;

namespace Studenda.Core.Model;

/// <summary>
/// Модель стандартного объекта с соответствующей
/// таблицей в базе данных.
/// Реализация Table Per Class подхода.
/// </summary>
public abstract class Entity
{
	/// <summary>
	/// Конфигурация модели <see cref="Entity"/>.
	/// Используется для дополнительной настройки,
	/// включая биндинг полей под данные,
	/// создание зависимостей и маппинг в базе данных.
	/// </summary>
	/// <typeparam name="T"><see cref="Entity"/></typeparam>
	internal abstract class Configuration<T> : IEntityTypeConfiguration<T> where T : Entity
	{
		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="configuration">Конфигурация базы данных.</param>
		protected Configuration(ContextConfiguration configuration)
		{
			ContextConfiguration = configuration;
		}

		/// <summary>
		/// Конфигурация базы данных.
		/// </summary>
		private ContextConfiguration ContextConfiguration { get; set; }

		/// <summary>
		/// Задать конфигурацию для модели.
		/// </summary>
		/// <param name="builder">Набор интерфейсов настройки модели.</param>
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
			builder.Property(entity => entity.CreatedAt)
				.HasColumnType(ContextConfiguration.DateTimeType)
				.HasDefaultValueSql(ContextConfiguration.DateTimeValueCurrent);

			builder.Property(entity => entity.UpdatedAt)
				.HasColumnType(ContextConfiguration.DateTimeType);
		}
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
	/// Статус необходимости наличия значения в поле <see cref="CreatedAt"/>.
	/// </summary>
	private const bool IsCreatedAtRequired = false;

	/// <summary>
	/// Статус необходимости наличия значения в поле <see cref="UpdatedAt"/>.
	/// </summary>
	private const bool IsUpdatedAtRequired = false;
	
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
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

	/// <summary>
	/// Дата создания.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Дата обновления.
	/// </summary>
	public DateTime? UpdatedAt { get; set; }

    #endregion
}
