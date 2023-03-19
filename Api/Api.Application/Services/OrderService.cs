using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dal.Interfaces;
using Domain.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddOrder(OrderData order)
    {
        var model = _mapper.Map<Order>(order);
        await _unitOfWork.GetRepository<Order>().AddAsync(model);
        await _unitOfWork.SaveChanges();
    }

    public async Task<OrderData[]> GetOrders()
    {
        var query = (await _unitOfWork
                .GetRepository<Order>()
                .GetAllAsync())
            .Include(x => x.OrderItems);

        return query
            .ProjectTo<OrderData>(_mapper.ConfigurationProvider)
            .ToArray();
    }
}