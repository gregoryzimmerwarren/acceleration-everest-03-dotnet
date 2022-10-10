using AppModels.Products;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System.Collections.Generic;

namespace AppServices.Services;

public class ProductAppService : IProductAppService
{
    private readonly IPortfolioProductService _portfolioProductService;
    private readonly IPortfolioService _portfolioService;
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public ProductAppService(
        IPortfolioProductService portfolioProductService,
        IPortfolioService portfolioService,
        IProductService productService,
        IOrderService orderService,
        IMapper mapper)
    {
        _portfolioProductService = portfolioProductService ?? throw new System.ArgumentNullException(nameof(portfolioProductService));
        _portfolioService = portfolioService ?? throw new System.ArgumentNullException(nameof(portfolioService));
        _productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        _orderService = orderService ?? throw new System.ArgumentNullException(nameof(orderService));
        _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    public long Create(CreateProductDto createProductDto)
    {
        var productMapeado = _mapper.Map<Product>(createProductDto);

        return _productService.Create(productMapeado);
    }

    public void Delete(long productId)
    {
        _productService.Delete(productId);
    }

    public IEnumerable<ProductResultDto> GetAllProducts()
    {
        var products = _productService.GetAllProducts();

        foreach (Product product in products)
        {
            var portfolios = _portfolioProductService.GetPortfolioProductByProductId(product.Id);
            product.Portfolios = _mapper.Map<ICollection<Portfolio>>(portfolios);

            var orders = _orderService.GetOrdersByPortfolioId(product.Id);
            product.Orders = _mapper.Map<List<Order>>(orders);
        }

        return _mapper.Map<IEnumerable<ProductResultDto>>(products);
    }

    public ProductResultDto GetProductById(long productId)
    {
        var product = _productService.GetProductById(productId);

        var portfolios = _portfolioProductService.GetPortfolioProductByProductId(product.Id);
        product.Portfolios = _mapper.Map<List<Portfolio>>(portfolios);

        var orders = _orderService.GetOrdersByPortfolioId(product.Id);
        product.Orders = _mapper.Map<List<Order>>(orders);

        return _mapper.Map<ProductResultDto>(product);
    }

    public void Update(long productId, UpdateProductDto updateProductDto)
    {
        var productMapeado = _mapper.Map<Product>(updateProductDto);
        productMapeado.Id = productId;

        _productService.Update(productMapeado);
    }
}