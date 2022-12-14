using AppModels.Orders;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResult>();
        CreateMap<Order, OrderResultForOtherDtos>();
        CreateMap<CreateOrder, Order>();
        CreateMap<UpdateOrder, Order>();
    }
}