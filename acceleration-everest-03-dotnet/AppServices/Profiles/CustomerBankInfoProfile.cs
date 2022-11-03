using AppModels.CustomersBankInfo;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles;

public class CustomerBankInfoProfile : Profile
{
    public CustomerBankInfoProfile()
    {
        CreateMap<CustomerBankInfo, CustomerBankInfoResult>();
        CreateMap<CustomerBankInfo, CustomerBankInfoResultForOthersDtos>();
    }
}