namespace Domain.Dto;

/// <summary>
/// Заказ
/// </summary>
public class OrderData
{
    /// <summary>
    /// Номер
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// Дата
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Идентификатор поставщика
    /// </summary>
    public int ProviderId { get; set; }

    /// <summary>
    /// Детали заказа
    /// </summary>
    public IList<OrderItemData> OrderItems { get; set; }
}