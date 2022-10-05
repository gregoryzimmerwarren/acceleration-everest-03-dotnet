using AppModels.Orders;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResultDto>();
        CreateMap<CreateOrderDto, Order>();
    }
}