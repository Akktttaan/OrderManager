using AutoMapper;
using Domain.Dto;
using Domain.Models;

namespace Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderData>().ReverseMap();
        CreateMap<Provider, ProviderData>().ReverseMap();
        CreateMap<OrderItem, OrderItemData>().ReverseMap();
    }
}