using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dal.Interfaces;
using Domain.Dto;
using Domain.FilterModels;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

/// <summary>
/// Сервис для заказов
/// </summary>
public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Добавить заказ
    /// </summary>
    public async Task AddOrder(OrderData order)
    {
        var model = _mapper.Map<Order>(order);
        await _unitOfWork.GetRepository<Order>().AddAsync(model);
        await _unitOfWork.SaveChanges();
    }

    public async Task UpdateOrder(OrderViewData order)
    {
        var entity = _mapper.Map<Order>(order);

        _unitOfWork.GetRepository<Order>().Update(entity);
        await _unitOfWork.SaveChanges();
    }

    /// <summary>
    /// Получить отфильтрованные заказы
    /// </summary>
    public async Task<OrderViewData[]> GetOrders(OrderFilterModel filterModel)
    {
        var orders = _unitOfWork
            .GetRepository<Order>()
            .GetAll();
        if (filterModel.DateFrom is not null && filterModel.DateTo is not null)
        {
            orders = orders.Where(x => x.Date >= filterModel.DateFrom && x.Date <= filterModel.DateTo);
        }
        else if (filterModel.OrderNumbers is not null)
        {
            orders = orders.Where(x => filterModel.OrderNumbers.Contains(x.Number));
        }
        else if (filterModel.ProviderIds is not null)
        {
            orders = orders.Where(x => filterModel.ProviderIds.Contains(x.ProviderId));
        }
        else if (filterModel.OrderItemUnits is not null)
        {
            orders = orders.Where(x => x.OrderItems.Any(y => filterModel.OrderItemUnits.Contains(y.Unit)));
        }
        else if (filterModel.OrderItemNames is not null)
        {
            orders = orders.Where(x => x.OrderItems.Any(y => filterModel.OrderItemNames.Contains(y.Name)));
        }
        else if (filterModel.ProviderNames is not null)
        {
            orders = orders.Where(x => filterModel.ProviderNames.Contains(x.Provider.Name));
        }

        var result = await orders
            .Include(x => x.Provider)
            .Include(x => x.OrderItems)
            .ProjectTo<OrderViewData>(_mapper.ConfigurationProvider)
            .ToArrayAsync();


        return result;
    }
    
    /// <summary>
    /// Получить заказы
    /// </summary>
    public async Task<OrderViewData[]> GetOrders()
    {
        var result = await _unitOfWork
            .GetRepository<Order>()
            .GetAll()
            .Include(x => x.OrderItems)
            .Include(x => x.Provider)
            .ProjectTo<OrderViewData>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return result.ToArray();
    }

    /// <summary>
    /// Удалить строку из заказа
    /// </summary>
    public async Task DeleteOrderItem(int id)
    {
        var entity = await _unitOfWork
            .GetRepository<OrderItem>()
            .FirstOrDefaultAsync(x => x.Id == id);
        _unitOfWork.GetRepository<OrderItem>().Delete(entity);
        await _unitOfWork.SaveChanges();
    }

    public async Task<OrderFilterModel> GetDataForFilterOrder()
    {
        var orders = await _unitOfWork.GetRepository<Order>().GetAll().ToListAsync();
        var orderItems = await _unitOfWork.GetRepository<OrderItem>().GetAll().ToListAsync();
        var providers = await _unitOfWork.GetRepository<Provider>().GetAll().ToListAsync();

        var filterModel = new OrderFilterModel()
        {
            OrderNumbers = orders.Select(x => x.Number).Distinct().ToArray(),
            ProviderIds = orders.Select(x => x.ProviderId).Distinct().ToArray(),
            OrderItemNames = orderItems.Select(x => x.Name).Distinct().ToArray(),
            OrderItemUnits = orderItems.Select(x => x.Unit).Distinct().ToArray(),
            ProviderNames = providers.Select(x => x.Name).Distinct().ToArray()
        };

        return filterModel;
    }
}