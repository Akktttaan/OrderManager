using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

/// <summary>
/// Детали заказа
/// </summary>
public class OrderItem : BaseEntity<int>
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    [Column(TypeName = "decimal(18,3)")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Единица
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// Ссылка на заказ
    /// </summary>
    public virtual Order Order { get; set; }
}