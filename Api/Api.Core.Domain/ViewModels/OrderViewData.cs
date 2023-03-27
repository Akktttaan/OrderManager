namespace Domain.ViewModels;

public class OrderViewData
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int? Id { get; set; }
    
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
    /// Поставщик
    /// </summary>
    public ProviderViewData Provider { get; set; }

    /// <summary>
    /// Детали заказа
    /// </summary>
    public IList<OrderItemViewData> OrderItems { get; set; }
}