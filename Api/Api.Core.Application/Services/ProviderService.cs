using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dal.Interfaces;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

/// <summary>
/// Сервис для поставщиков
/// </summary>
public class ProviderService : IProviderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProviderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<ProviderViewData[]> GetProviders()
    {
        var result = await _unitOfWork
            .GetRepository<Provider>()
            .GetAll()
            .ProjectTo<ProviderViewData>(_mapper.ConfigurationProvider)
            .ToArrayAsync();

        return result;
    }
}