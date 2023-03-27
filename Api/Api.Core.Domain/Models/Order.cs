namespace Domain.Models;

/// <summary>
/// Заказ
/// </summary>
public class Order : BaseEntity<int>
{
    /// <summary>
    /// Номер
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// Дата
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Идентификатор поставщика
    /// </summary>
    public int ProviderId { get; set; }

    /// <summary>
    /// Ссылкан на поставщика
    /// </summary>
    public virtual Provider Provider { get; set; }

    /// <summary>
    /// Детали заказа
    /// </summary>
    public virtual IList<OrderItem> OrderItems { get; set; }
}