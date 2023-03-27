using Domain.Interfaces;

namespace Domain;

/// <summary>
/// Базовый класс для моделей
/// </summary>
/// <typeparam name="T">Тип первичного ключа</typeparam>
public abstract class BaseEntity<T> : IBaseEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public T Id { get; set; }
}