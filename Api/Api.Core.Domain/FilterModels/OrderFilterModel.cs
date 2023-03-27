namespace Domain.FilterModels;

/// <summary>
/// Модель для фильтрации заказов
/// </summary>
public class OrderFilterModel
{
    /// <summary>
    /// Дата от
    /// </summary>
    public DateTime? DateFrom { get; set; }
    
    /// <summary>
    /// Дата до
    /// </summary>
    public DateTime? DateTo { get; set; }
    
    /// <summary>
    /// Номера заказов
    /// </summary>
    public string[]? OrderNumbers { get; set; }
    
    /// <summary>
    /// Идентификаторы поставщика
    /// </summary>
    public int[]? ProviderIds { get; set; }
    
    /// <summary>
    /// Единица деталей заказа
    /// </summary>
    public string[]? OrderItemUnits { get; set; }
    
    /// <summary>
    /// Наименование деталей заказа
    /// </summary>
    public string[]? OrderItemNames { get; set; }

    /// <summary>
    /// Наименования поставщиков
    /// </summary>
    public string[]? ProviderNames { get; set; }
}