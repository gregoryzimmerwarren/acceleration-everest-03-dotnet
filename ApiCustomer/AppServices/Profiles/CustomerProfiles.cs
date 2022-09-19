using AppModels;
using AutoMapper;
using DomainModels;

namespace AppServices.Profiles;

public class CustomerProfiles : Profile
{
    public CustomerProfiles()
    {
        CreateMap<Customer, ResultCustomerDto>();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>();
    }
}
