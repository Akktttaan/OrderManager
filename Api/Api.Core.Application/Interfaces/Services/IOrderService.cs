using Domain.Dto;
using Domain.FilterModels;
using Domain.ViewModels;

namespace Application.Interfaces.Services;

public interface IOrderService : IService
{
    Task<OrderViewData[]> GetOrders(OrderFilterModel filterModel);
    
    Task<OrderViewData[]> GetOrders();

    Task AddOrder(OrderData order);
    Task UpdateOrder(OrderViewData order);

    Task DeleteOrderItem(int id);

    Task<OrderFilterModel> GetDataForFilterOrder();
}