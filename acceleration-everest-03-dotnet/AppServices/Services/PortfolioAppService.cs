using AppModels.Orders;
using AppModels.Portfolios;
using AppModels.PortfoliosProducts;
using AppModels.Products;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Enums;
using DomainModels.Models;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;

namespace AppServices.Services;

public class PortfolioAppService : IPortfolioAppService
{
    private readonly ICustomerBankInfoAppService _customerBankInfoAppService;
    private readonly IPortfolioProductAppService _portfolioProductAppService;
    private readonly IPortfolioProductService _portfolioProductService;
    private readonly ICustomerAppService _customerAppService;
    private readonly IProductAppService _productAppService;
    private readonly IPortfolioService _portfolioService;
    private readonly IOrderAppService _orderAppService;
    private readonly IMapper _mapper;

    public PortfolioAppService(
        ICustomerBankInfoAppService customerBankInfoAppService,
        IPortfolioProductAppService portfolioProductAppService,
        IPortfolioProductService portfolioProductService,
        ICustomerAppService customerAppService,
        IProductAppService productAppService,
        IPortfolioService portfolioService,
        IOrderAppService orderAppService,
        IMapper mapper)
    {
        _customerBankInfoAppService = customerBankInfoAppService ?? throw new System.ArgumentNullException(nameof(customerBankInfoAppService));
        _portfolioProductAppService = portfolioProductAppService ?? throw new System.ArgumentNullException(nameof(portfolioProductAppService));
        _portfolioProductService = portfolioProductService ?? throw new System.ArgumentNullException(nameof(portfolioProductService));
        _customerAppService = customerAppService ?? throw new System.ArgumentNullException(nameof(customerAppService));
        _productAppService = productAppService ?? throw new System.ArgumentNullException(nameof(productAppService));
        _portfolioService = portfolioService ?? throw new System.ArgumentNullException(nameof(portfolioService));
        _orderAppService = orderAppService ?? throw new System.ArgumentNullException(nameof(orderAppService));
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

    public void Deposit(long customerId, long portfolioId, decimal amount, bool amountInBankInfo)
    {
        if (amountInBankInfo == false)
            _customerBankInfoAppService.Deposit(customerId, amount);

        _customerBankInfoAppService.Withdraw(customerId, amount);
        _portfolioService.Deposit(portfolioId, amount);
    }

    public IEnumerable<PortfolioResultDto> GetAllPortfolios()
    {
        var portfolios = _portfolioService.GetAllPortfolios();

        foreach (Portfolio portfolio in portfolios)
        {
            var customer = _customerAppService.GetCustomerById(portfolio.CustomerId);
            portfolio.Customer = _mapper.Map<Customer>(customer);

            IEnumerable<PortfolioProduct> portfoliosproducts;

            try
            {
                portfoliosproducts = _portfolioProductService.GetPortfolioProductByPortfolioId(portfolio.Id);
            }
            catch (ArgumentException exception)
            {
                continue;
            }

            List<ProductResultDto> products = new();

            foreach (PortfolioProduct portfolioproduct in portfoliosproducts)
            {
                try
                {
                    var product = _productAppService.GetProductById(portfolioproduct.ProductId);

                    products.Add(product);
                }
                catch (ArgumentException exception)
                {
                    continue;
                }
            }

            portfolio.Products = _mapper.Map<List<Product>>(products);

            try
            {
                var orders = _orderAppService.GetOrdersByPortfolioId(portfolio.Id);
                portfolio.Orders = _mapper.Map<List<Order>>(orders);
            }
            catch (ArgumentException exception)
            {
                portfolio.Orders = new List<Order>();
            }
        }

        return _mapper.Map<IEnumerable<PortfolioResultDto>>(portfolios);
    }

    public PortfolioResultDto GetPortfolioById(long portfolioId)
    {
        var portfolio = _portfolioService.GetPortfolioById(portfolioId);

        var customer = _customerAppService.GetCustomerById(portfolio.CustomerId);
        portfolio.Customer = _mapper.Map<Customer>(customer);

        var portfoliosproducts = _portfolioProductService.GetPortfolioProductByPortfolioId(portfolio.Id);

        List<ProductResultDto> products = new();

        foreach (PortfolioProduct portfolioproduct in portfoliosproducts)
        {
            var product = _productAppService.GetProductById(portfolioproduct.ProductId);

            products.Add(product);
        }

        portfolio.Products = _mapper.Map<List<Product>>(products);

        var orders = _orderAppService.GetOrdersByPortfolioId(portfolio.Id);
        portfolio.Orders = _mapper.Map<List<Order>>(orders);

        return _mapper.Map<PortfolioResultDto>(portfolio);
    }

    public IEnumerable<PortfolioResultDto> GetPortfoliosByCustomerId(long customerId)
    {
        var portfolios = _portfolioService.GetPortfoliosByCustomerId(customerId);

        foreach (Portfolio portfolio in portfolios)
        {
            var customer = _customerAppService.GetCustomerById(portfolio.CustomerId);
            portfolio.Customer = _mapper.Map<Customer>(customer);

            var portfoliosproducts = _portfolioProductService.GetPortfolioProductByPortfolioId(portfolio.Id);

            List<ProductResultDto> products = new();

            foreach (PortfolioProduct portfolioproduct in portfoliosproducts)
            {
                var product = _productAppService.GetProductById(portfolioproduct.ProductId);

                products.Add(product);
            }

            portfolio.Products = _mapper.Map<List<Product>>(products);

            var orders = _orderAppService.GetOrdersByPortfolioId(portfolio.Id);
            portfolio.Orders = _mapper.Map<List<Order>>(orders);
        }

        return _mapper.Map<IEnumerable<PortfolioResultDto>>(portfolios);
    }

    public bool Invest(CreateOrderDto createOrderDto, long portfolioId, long productId, decimal amount)
    {
        var product = _productAppService.GetProductById(productId);

        if (product != null)
        {
            createOrderDto.Direction = OrderDirection.Buy;
            _orderAppService.Create(createOrderDto);
        }

        var result = _portfolioService.Invest(portfolioId, amount);

        _portfolioProductAppService.Create(new CreatePortfolioProductDto(portfolioId, productId));

        return result;
    }

    public bool RedeemToPortfolio(CreateOrderDto createOrderDto, long portfolioId, long productId, decimal amount)
    {
        createOrderDto.Direction = OrderDirection.Sell;
        _orderAppService.Create(createOrderDto);

        var result = _portfolioService.RedeemToPortfolio(portfolioId, amount);

        _portfolioProductAppService.Delete(portfolioId, productId);

        return result;
    }

    public bool WithdrawFromPortfolio(long portfolioId, decimal amount)
    {
        var result = _portfolioService.WithdrawFromPortfolio(portfolioId, amount);

        return result;
    }
}