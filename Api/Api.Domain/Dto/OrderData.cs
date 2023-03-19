namespace Domain.Dto;

/// <summary>
/// Заказ
/// </summary>
public class OrderData
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
}