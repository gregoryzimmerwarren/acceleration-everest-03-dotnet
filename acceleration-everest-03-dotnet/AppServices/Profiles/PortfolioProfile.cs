using AppModels.Portfolios;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles;

public class PortfolioProfile : Profile
{   
    public PortfolioProfile()
    {
        CreateMap<Portfolio, PortfolioResult>();
        CreateMap<Portfolio, PortfolioResultForOthersDtos>();
        CreateMap<CreatePortfolio, Portfolio>();
    }
}