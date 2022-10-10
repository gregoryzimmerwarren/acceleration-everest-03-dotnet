using AppModels.PortfoliosProducts;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System.Collections.Generic;

namespace AppServices.Services;

public class PortfolioProductAppService : IPortfolioProductAppService
{
    private readonly IPortfolioProductService _portfolioProductService;
    private readonly IPortfolioService _portfolioService;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public PortfolioProductAppService(
        IPortfolioProductService portfolioProductService, 
        IPortfolioService portfolioService, 
        IProductService productService, 
        IMapper mapper)
    {
        _portfolioProductService = portfolioProductService ?? throw new System.ArgumentNullException(nameof(portfolioProductService));
        _portfolioService = portfolioService ?? throw new System.ArgumentNullException(nameof(portfolioService));
        _productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    public long Create(CreatePortfolioProductDto createPortfolioProductDto)
    {
        var portfolioProductMapeado = _mapper.Map<PortfolioProduct>(createPortfolioProductDto);

        return _portfolioProductService.Create(portfolioProductMapeado);
    }

    public void Delete(long portfolioId, long ProductId)
    {
        _portfolioProductService.Delete(portfolioId, ProductId);
    }

    public IEnumerable<PortfolioProductResultDto> GetAllPortfolioProduct()
    {
        var portfoliosProducts = _portfolioProductService.GetAllPortfolioProduct();

        foreach (PortfolioProduct portfolioProduct in portfoliosProducts)
        {
            var portfolio = _portfolioService.GetPortfolioById(portfolioProduct.PortfolioId);
            portfolioProduct.Portfolio = _mapper.Map<Portfolio>(portfolio);
            
            var product = _productService.GetProductById(portfolioProduct.ProductId);
            portfolioProduct.Product = _mapper.Map<Product>(product);
        }

        return _mapper.Map<IEnumerable<PortfolioProductResultDto>>(portfoliosProducts);
    }

    public PortfolioProductResultDto GetPortfolioProductByIds(long portfolioId, long ProductId)
    {
        var portfolioProduct = _portfolioProductService.GetPortfolioProductByIds(portfolioId, ProductId);

        var portfolio = _portfolioService.GetPortfolioById(portfolioProduct.PortfolioId);
        portfolioProduct.Portfolio = _mapper.Map<Portfolio>(portfolio);

        var product = _productService.GetProductById(portfolioProduct.ProductId);
        portfolioProduct.Product = _mapper.Map<Product>(product);

        return _mapper.Map<PortfolioProductResultDto>(portfolioProduct);
    }

    public IEnumerable<PortfolioProductResultDto> GetPortfolioProductByProductId(long productId)
    {
        var portfoliosProducts = _portfolioProductService.GetPortfolioProductByProductId(productId);

        foreach (PortfolioProduct portfolioProduct in portfoliosProducts)
        {
            var portfolio = _portfolioService.GetPortfolioById(portfolioProduct.PortfolioId);
            portfolioProduct.Portfolio = _mapper.Map<Portfolio>(portfolio);

            var product = _productService.GetProductById(portfolioProduct.ProductId);
            portfolioProduct.Product = _mapper.Map<Product>(product);
        }

        return _mapper.Map<IEnumerable<PortfolioProductResultDto>>(portfoliosProducts);
    }

    public IEnumerable<PortfolioProductResultDto> GetPortfolioProductByPortfolioId(long portfolioId)
    {
        var portfoliosProducts = _portfolioProductService.GetPortfolioProductByPortfolioId(portfolioId);

        foreach (PortfolioProduct portfolioProduct in portfoliosProducts)
        {
            var portfolio = _portfolioService.GetPortfolioById(portfolioProduct.PortfolioId);
            portfolioProduct.Portfolio = _mapper.Map<Portfolio>(portfolio);

            var product = _productService.GetProductById(portfolioProduct.ProductId);
            portfolioProduct.Product = _mapper.Map<Product>(product);
        }

        return _mapper.Map<IEnumerable<PortfolioProductResultDto>>(portfoliosProducts);
    }
}
