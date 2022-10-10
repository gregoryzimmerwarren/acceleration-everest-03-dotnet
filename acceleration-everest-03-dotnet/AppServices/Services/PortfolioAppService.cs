using AppModels.Portfolios;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System.Collections.Generic;

namespace AppServices.Services;

public class PortfolioAppService : IPortfolioAppService
{
    private readonly IPortfolioProductService _portfolioProductService;
    private readonly IPortfolioService _portfolioService;
    private readonly ICustomerService _customerService;
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public PortfolioAppService(
        IPortfolioProductService portfolioProductService,
        IPortfolioService portfolioService,
        ICustomerService customerService,
        IProductService productService,
        IOrderService orderService,
        IMapper mapper)
    {
        _portfolioProductService = portfolioProductService ?? throw new System.ArgumentNullException(nameof(portfolioProductService));
        _portfolioService = portfolioService ?? throw new System.ArgumentNullException(nameof(portfolioService));
        _customerService = customerService ?? throw new System.ArgumentNullException(nameof(customerService));
        _productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        _orderService = orderService ?? throw new System.ArgumentNullException(nameof(orderService));
        _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    public long Create(CreatePortfolioDto createPortfolioDto)
    {
        var portfolioMapeado = _mapper.Map<Portfolio>(createPortfolioDto);

        return _portfolioService.Create(portfolioMapeado);
    }

    public void Delete(long portfolioId)
    {
        _portfolioService.Delete(portfolioId);
    }

    public void Deposit(long portfolioId, decimal amount)
    {
        _portfolioService.Deposit(portfolioId, amount);
    }

    public IEnumerable<PortfolioResultDto> GetAllPortfolios()
    {
        var portfolios = _portfolioService.GetAllPortfolios();

        foreach (Portfolio portfolio in portfolios)
        {
            var customer = _customerService.GetCustomerById(portfolio.CustomerId);
            portfolio.Customer = _mapper.Map<Customer>(customer);

            var portfoliosproducts = _portfolioProductService.GetPortfolioProductByPortfolioId(portfolio.Id);

            List<Product> products = new();

            foreach (PortfolioProduct portfolioproduct in portfoliosproducts)
            {
                var product = _productService.GetProductById(portfolioproduct.ProductId);

                products.Add(product);
            }

            portfolio.Products = _mapper.Map<List<Product>>(products);

            var orders = _orderService.GetOrdersByPortfolioId(portfolio.Id);
            portfolio.Orders = _mapper.Map<List<Order>>(orders);
        }

        return _mapper.Map<IEnumerable<PortfolioResultDto>>(portfolios);
    }

    public PortfolioResultDto GetPortfolioById(long portfolioId)
    {
        var portfolio = _portfolioService.GetPortfolioById(portfolioId);

        var customer = _customerService.GetCustomerById(portfolio.CustomerId);
        portfolio.Customer = _mapper.Map<Customer>(customer);

        var portfoliosproducts = _portfolioProductService.GetPortfolioProductByPortfolioId(portfolio.Id);

        List<Product> products = new();

        foreach (PortfolioProduct portfolioproduct in portfoliosproducts)
        {
            var product = _productService.GetProductById(portfolioproduct.ProductId);

            products.Add(product);
        }

        portfolio.Products = _mapper.Map<List<Product>>(products);

        var orders = _orderService.GetOrdersByPortfolioId(portfolio.Id);
        portfolio.Orders = _mapper.Map<List<Order>>(orders);

        return _mapper.Map<PortfolioResultDto>(portfolio);
    }

    public IEnumerable<PortfolioResultDto> GetPortfoliosByCustomerId(long customerId)
    {
        var portfolios = _portfolioService.GetPortfoliosByCustomerId(customerId);

        foreach (Portfolio portfolio in portfolios)
        {
            var customer = _customerService.GetCustomerById(portfolio.CustomerId);
            portfolio.Customer = _mapper.Map<Customer>(customer);

            var portfoliosproducts = _portfolioProductService.GetPortfolioProductByPortfolioId(portfolio.Id);

            List<Product> products = new();

            foreach (PortfolioProduct portfolioproduct in portfoliosproducts)
            {
                var product = _productService.GetProductById(portfolioproduct.ProductId);

                products.Add(product);
            }

            portfolio.Products = _mapper.Map<List<Product>>(products);

            var orders = _orderService.GetOrdersByPortfolioId(portfolio.Id);
            portfolio.Orders = _mapper.Map<List<Order>>(orders);
        }

        return _mapper.Map<IEnumerable<PortfolioResultDto>>(portfolios);
    }

    public bool Invest(long portfolioId, decimal amount)
    {
        var result = _portfolioService.Invest(portfolioId, amount);

        return result;
    }

    public bool RedeemToPortfolio(long portfolioId, decimal amount)
    {
        var result = _portfolioService.RedeemToPortfolio(portfolioId, amount);

        return result;
    }

    public bool WithdrawFromPortfolio(long portfolioId, decimal amount)
    {
        var result = _portfolioService.WithdrawFromPortfolio(portfolioId, amount);

        return result;
    }
}