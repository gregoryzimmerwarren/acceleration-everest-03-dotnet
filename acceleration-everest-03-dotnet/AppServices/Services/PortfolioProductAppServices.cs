using AppModels.PortfoliosProducts;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System.Collections.Generic;

namespace AppServices.Services;

public class PortfolioProductAppServices : IPortfolioProductAppServices
{
    private readonly IPortfolioProductService _portfolioProductService;
    private readonly IMapper _mapper;

    public PortfolioProductAppServices(IPortfolioProductService portfolioProductService, IMapper mapper)
    {
        _portfolioProductService = portfolioProductService;
        _mapper = mapper;
    }

    public long Create(CreatePortfolioProductDto createPortfolioProductDto)
    {
        var portfolioProductMapeado = _mapper.Map<PortfolioProduct>(createPortfolioProductDto);

        return _portfolioProductService.Create(portfolioProductMapeado);
    }

    public void Delete(long portfolioProductId)
    {
        _portfolioProductService.Delete(portfolioProductId);
    }

    public IEnumerable<PortfolioProductResultDto> GetAllPortfolioProduct()
    {
        var portfoliosProducts = _portfolioProductService.GetAllPortfolioProduct();

        return _mapper.Map<IEnumerable<PortfolioProductResultDto>>(portfoliosProducts);
    }

    public PortfolioProductResultDto GetPortfolioProductById(long portfolioProductId)
    {
        var portfolioProduct = _portfolioProductService.GetPortfolioProductById(portfolioProductId);

        return _mapper.Map<PortfolioProductResultDto>(portfolioProduct);
    }

    public IEnumerable<PortfolioProductResultDto> GetPortfoliosByProductId(long productId)
    {
        var portfoliosProducts = _portfolioProductService.GetPortfoliosByProductId(productId);

        return _mapper.Map<IEnumerable<PortfolioProductResultDto>>(portfoliosProducts);
    }

    public IEnumerable<PortfolioProductResultDto> GetProductsByPortfolioId(long portfolioId)
    {
        var portfoliosProducts = _portfolioProductService.GetProductsByPortfolioId(portfolioId);

        return _mapper.Map<IEnumerable<PortfolioProductResultDto>>(portfoliosProducts);
    }
}
