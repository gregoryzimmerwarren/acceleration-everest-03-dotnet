using AppModels.PortfoliosProducts;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles;

public class PortfolioProductProfile : Profile
{
    public PortfolioProductProfile()
    {
        CreateMap<PortfolioProduct, PortfolioProductResult>();
        CreateMap<CreatePortfolioProduct, PortfolioProduct>();
    }
}