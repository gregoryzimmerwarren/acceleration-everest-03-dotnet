using AppModels.DTOs;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles
{
    public class CustomerProfiles : Profile
    {
        public CustomerProfiles()
        {
            CreateMap<CustomerModel, GetCustomerDto>();
            CreateMap<PostCustomerDto, CustomerModel>();
            CreateMap<PutCustomerDto, CustomerModel>();
        }
    }
}
