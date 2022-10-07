﻿using AppModels.Portfolios;
using AppModels.Products;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles;

public class PortfolioProfile : Profile
{   
    public PortfolioProfile()
    {
        CreateMap<Portfolio, PortfolioResultDto>();
        CreateMap<Portfolio, PortfolioResultForOthersDtos>();
        CreateMap<CreatePortfolioDto, Portfolio>();
    }
}