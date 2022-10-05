using AppModels.Portfolios;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles;

public class PortfolioProfile : Profile
{   
    public PortfolioProfile()
    {
        CreateMap<Portfolio, PortfolioResultDto>();
        CreateMap<CreatePortfolioDto, Portfolio>();
    }
}