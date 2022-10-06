using AppModels.CustomersBankInfo;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles;

public class CustomerBankInfoProfile : Profile
{
    public CustomerBankInfoProfile()
    {
        CreateMap<CustomerBankInfo, CustomerBankInfoResultDto>();
        CreateMap<CreateCustomerBankInfoDto, CustomerBankInfo>();
    }
}