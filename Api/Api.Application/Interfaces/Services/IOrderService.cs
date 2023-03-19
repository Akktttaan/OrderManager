using Domain.Dto;
using Domain.Models;

namespace Application.Interfaces.Services;

public interface IOrderService : IService
{
    Task<OrderData[]> GetOrders();

    Task AddOrder(OrderData order);
}