namespace Domain.Models;

/// <summary>
/// Поставщик
/// </summary>
public class Provider : BaseEntity<int>
{
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; }
}