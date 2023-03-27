using AutoMapper;
using Domain.Dto;
using Domain.Models;
using Domain.ViewModels;

namespace Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderData>().ReverseMap();
        CreateMap<Order, OrderViewData>().ReverseMap();
        CreateMap<Provider, ProviderViewData>().ReverseMap();
        CreateMap<OrderItem, OrderItemData>().ReverseMap();
        CreateMap<OrderItem, OrderItemViewData>().ReverseMap();
    }
}