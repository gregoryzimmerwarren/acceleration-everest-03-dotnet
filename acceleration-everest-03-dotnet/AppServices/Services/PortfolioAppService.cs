using AppModels.Orders;
using AppModels.Portfolios;
using AppModels.PortfoliosProducts;
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
    private readonly IPortfolioProductAppService _portfolioProductAppService;
    private readonly ICustomerBankInfoAppService _customerBankInfoAppService;
    private readonly IPortfolioProductService _portfolioProductService;
    private readonly IPortfolioService _portfolioService;
    private readonly IOrderAppService _orderAppService;
    private readonly ICustomerService _customerService;
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public PortfolioAppService(
        IPortfolioProductAppService portfolioProductAppService,
        ICustomerBankInfoAppService customerBankInfoService,
        IPortfolioProductService portfolioProductService,
        IPortfolioService portfolioService,
        IOrderAppService orderAppService,
        ICustomerService customerService,
        IProductService productService,
        IOrderService orderService,
        IMapper mapper)
    {
        _portfolioProductAppService = portfolioProductAppService ?? throw new System.ArgumentNullException(nameof(portfolioProductAppService));
        _customerBankInfoAppService = customerBankInfoService ?? throw new System.ArgumentNullException(nameof(customerBankInfoService));
        _portfolioProductService = portfolioProductService ?? throw new System.ArgumentNullException(nameof(portfolioProductService));
        _portfolioService = portfolioService ?? throw new System.ArgumentNullException(nameof(portfolioService));
        _orderAppService = orderAppService ?? throw new System.ArgumentNullException(nameof(orderAppService));
        _customerService = customerService ?? throw new System.ArgumentNullException(nameof(customerService));
        _productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        _orderService = orderService ?? throw new System.ArgumentNullException(nameof(orderService));
        _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    public long Create(CreatePortfolioDto createPortfolioDto)
    {
        var portfolioMapeado = _mapper.Map<Portfolio>(createPortfolioDto);
        portfolioMapeado.AccountBalance = 0;
        portfolioMapeado.TotalBalance = 0;

        return _portfolioService.Create(portfolioMapeado);
    }

    public void Delete(long portfolioId)
    {
        _portfolioService.Delete(portfolioId);
    }

    public string Deposit(long customerId, long portfolioId, decimal amount)
    {
        var totalInBankInfo = _customerBankInfoAppService.GetTotalByCustomerId(customerId);

        if (totalInBankInfo < amount)
        {
            _customerBankInfoAppService.Deposit(customerId, amount);

            return "Deposit made in customer bank info";
        }
        else
        {
            _customerBankInfoAppService.Withdraw(customerId, amount);
            _portfolioService.Deposit(portfolioId, amount);

            return "Deposit made in portfolio"; ;
        }        
    }

    public IEnumerable<PortfolioResultDto> GetAllPortfolios()
    {
        var portfolios = _portfolioService.GetAllPortfolios();

        foreach (Portfolio portfolio in portfolios)
        {
            var customer = _customerService.GetCustomerById(portfolio.CustomerId);
            portfolio.Customer = _mapper.Map<Customer>(customer);

            IEnumerable<PortfolioProduct> portfoliosproducts;

            try
            {
                portfoliosproducts = _portfolioProductService.GetPortfolioProductByPortfolioId(portfolio.Id);
            }
            catch (ArgumentException)
            {
                continue;
            }

            List<Product> products = new();

            foreach (PortfolioProduct portfolioproduct in portfoliosproducts)
            {
                try
                {
                    var product = _productService.GetProductById(portfolioproduct.ProductId);

                    products.Add(product);
                }
                catch (ArgumentException)
                {
                    continue;
                }
            }

            portfolio.Products = _mapper.Map<List<Product>>(products);

            try
            {
                var orders = _orderService.GetOrdersByPortfolioId(portfolio.Id);
                portfolio.Orders = _mapper.Map<List<Order>>(orders);
            }
            catch (ArgumentException)
            {
                portfolio.Orders = new List<Order>();
            }
        }

        return _mapper.Map<IEnumerable<PortfolioResultDto>>(portfolios);
    }

    public PortfolioResultDto GetPortfolioById(long portfolioId)
    {
        var portfolio = _portfolioService.GetPortfolioById(portfolioId);

        var customer = _customerService.GetCustomerById(portfolio.CustomerId);
        portfolio.Customer = _mapper.Map<Customer>(customer);

        try
        {
            var portfoliosproducts = _portfolioProductService.GetPortfolioProductByPortfolioId(portfolio.Id);

            List<Product> products = new();

            foreach (PortfolioProduct portfolioproduct in portfoliosproducts)
            {
                try
                {
                    var product = _productService.GetProductById(portfolioproduct.ProductId);

                    products.Add(product);
                }
                catch (ArgumentException)
                {
                    continue;
                }
            }

            portfolio.Products = _mapper.Map<List<Product>>(products);

            try
            {
                var orders = _orderService.GetOrdersByPortfolioId(portfolio.Id);
                portfolio.Orders = _mapper.Map<List<Order>>(orders);
            }
            catch (ArgumentException)
            {
                portfolio.Orders = new List<Order>();
            }

            return _mapper.Map<PortfolioResultDto>(portfolio);
        }
        catch (ArgumentException)
        {
            portfolio.Products = new List<Product>();
            portfolio.Orders = new List<Order>();

            return _mapper.Map<PortfolioResultDto>(portfolio);
        }
    }

    public IEnumerable<PortfolioResultDto> GetPortfoliosByCustomerId(long customerId)
    {
        var portfolios = _portfolioService.GetPortfoliosByCustomerId(customerId);

        foreach (Portfolio portfolio in portfolios)
        {
            var customer = _customerService.GetCustomerById(portfolio.CustomerId);
            portfolio.Customer = _mapper.Map<Customer>(customer);

            try
            {
                var portfoliosproducts = _portfolioProductService.GetPortfolioProductByPortfolioId(portfolio.Id);

                List<Product> products = new();

                foreach (PortfolioProduct portfolioproduct in portfoliosproducts)
                {
                    try
                    {
                        var product = _productService.GetProductById(portfolioproduct.ProductId);

                        products.Add(product);
                    }
                    catch (ArgumentException)
                    {
                        continue;
                    }
                }

                portfolio.Products = _mapper.Map<List<Product>>(products);
            }
            catch (ArgumentException)
            {
                portfolio.Products = new List<Product>();
            }

            try
            {
                var orders = _orderService.GetOrdersByPortfolioId(portfolio.Id);
                portfolio.Orders = _mapper.Map<List<Order>>(orders);
            }
            catch (ArgumentException)
            {
                portfolio.Orders = new List<Order>();
            }
        }

        return _mapper.Map<IEnumerable<PortfolioResultDto>>(portfolios);
    }

    public bool Invest(CreateOrderDto createOrderDto, decimal amount)
    {
        var product = _productService.GetProductById(createOrderDto.ProductId);

        if (product != null)
        {
            createOrderDto.Direction = OrderDirection.Buy;
            _orderAppService.Create(createOrderDto);
        }

        _portfolioProductAppService.Create(new CreatePortfolioProductDto(createOrderDto.PortfolioId, createOrderDto.ProductId));

        if (createOrderDto.LiquidatedAt > DateTime.Today)
            throw new ArgumentException($"The investment will only take place on the liquidation date: {createOrderDto.LiquidatedAt}");

        var result = _portfolioService.Invest(createOrderDto.PortfolioId, amount);

        return result;
    }

    public bool RedeemToPortfolio(CreateOrderDto createOrderDto, decimal amount)
    {
        createOrderDto.Direction = OrderDirection.Sell;
        _orderAppService.Create(createOrderDto);

        if (createOrderDto.LiquidatedAt > DateTime.Today)
            throw new ArgumentException($"The amount {amount} was not credited to the portfolio Id {createOrderDto.PortfolioId}. The order liquidate in a date greater than today");

        var result = _portfolioService.RedeemToPortfolio(createOrderDto.PortfolioId, amount);

        var orders = _orderService.GetOrderByPorfolioIdAndProductId(createOrderDto.PortfolioId, createOrderDto.ProductId);
        var sellingQuotes = createOrderDto.Quotes;
        var boughtQuotes = 0;

        foreach (Order order in orders)
        {
            if (order.Direction == OrderDirection.Buy)
            {
                boughtQuotes += order.Quotes;
            }
            else
            {
                sellingQuotes += order.Quotes;
            }
        }

        foreach (Order order in orders)
            if (boughtQuotes >= sellingQuotes)
                if (order.Direction == OrderDirection.Buy)
                {
                    _portfolioProductAppService.Delete(createOrderDto.PortfolioId, createOrderDto.ProductId);
                    boughtQuotes -= order.Quotes;
                }

        return result;
    }

    public bool WithdrawFromPortfolio(long customerId, long portfolioId, decimal amount)
    {
        var result = _portfolioService.WithdrawFromPortfolio(portfolioId, amount);
        _customerBankInfoAppService.Deposit(customerId, amount);

        return result;
    }
}