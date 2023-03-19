﻿namespace Domain.Dto;

/// <summary>
/// Детали заказа
/// </summary>
public class OrderItemData
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int? Id { get; set; }
    
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
    public decimal Quantity { get; set; }

    /// <summary>
    /// Единица
    /// </summary>
    public string Unit { get; set; }
}