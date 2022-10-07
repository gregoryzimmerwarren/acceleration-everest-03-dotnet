using AppModels.Customers;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerResultDto>();
        CreateMap<Customer, CustomerResultForOtherDtos>();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>();
    }
}