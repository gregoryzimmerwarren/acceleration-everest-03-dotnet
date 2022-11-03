using AppModels.Customers;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerResult>();
        CreateMap<Customer, CustomerResultForOtherDtos>();
        CreateMap<CreateCustomer, Customer>();
        CreateMap<UpdateCustomer, Customer>();
    }
}