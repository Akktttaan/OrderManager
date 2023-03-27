namespace Domain.Dto;

/// <summary>
/// Детали заказа
/// </summary>
public class OrderItemData
{
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// Единица
    /// </summary>
    public string Unit { get; set; }
}