using Domain.ViewModels;

namespace Application.Interfaces.Services;

/// <summary>
/// Сервис для поставщиков
/// </summary>
public interface IProviderService : IService
{
    Task<ProviderViewData[]> GetProviders();
}