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
using System.Threading.Tasks;

namespace AppServices.Services;

public class PortfolioAppService : IPortfolioAppService
{
    private readonly IPortfolioProductAppService _portfolioProductAppService;
    private readonly ICustomerBankInfoAppService _customerBankInfoAppService;
    private readonly IProductAppService _productAppService;
    private readonly IPortfolioService _portfolioService;
    private readonly IOrderAppService _orderAppService;
    private readonly IMapper _mapper;

    public PortfolioAppService(
        IPortfolioProductAppService portfolioProductAppService,
        ICustomerBankInfoAppService customerBankInfoService,
        IProductAppService productAppService,
        IPortfolioService portfolioService,
        IOrderAppService orderAppService,
        IMapper mapper)
    {
        _portfolioProductAppService = portfolioProductAppService ?? throw new System.ArgumentNullException(nameof(portfolioProductAppService));
        _customerBankInfoAppService = customerBankInfoService ?? throw new System.ArgumentNullException(nameof(customerBankInfoService));
        _productAppService = productAppService ?? throw new System.ArgumentNullException(nameof(productAppService));
        _portfolioService = portfolioService ?? throw new System.ArgumentNullException(nameof(portfolioService));
        _orderAppService = orderAppService ?? throw new System.ArgumentNullException(nameof(orderAppService));
        _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    public long Create(CreatePortfolio createPortfolioDto)
    {
        var mappedPortfolio = _mapper.Map<Portfolio>(createPortfolioDto);

        return _portfolioService.Create(mappedPortfolio);
    }

    public async Task DeleteAsync(long portfolioId)
    {
        await _portfolioService.DeleteAsync(portfolioId).ConfigureAwait(false);
    }

    public async Task DepositAsync(long customerId, long portfolioId, decimal amount)
    {
        var totalInBankInfo = await _customerBankInfoAppService.GetTotalByCustomerIdAsync(customerId).ConfigureAwait(false);

        if (totalInBankInfo < amount)
            throw new ArgumentException($"The customer bank info does not enough value to make this deposit. Current value: {totalInBankInfo}");

        await _customerBankInfoAppService.WithdrawAsync(customerId, amount).ConfigureAwait(false);
        await _portfolioService.DepositAsync(portfolioId, amount).ConfigureAwait(false);
    }

    public async Task ExecuteBuyOrderAsync(long portfolioId, long productId, decimal amount)
    {
        _portfolioProductAppService.Create(new CreatePortfolioProduct(portfolioId, productId));
        await _portfolioService.InvestAsync(portfolioId, amount).ConfigureAwait(false);
    }

    public async Task ExecuteOrdersOfTheDayAsync()
    {
        var orders = await _orderAppService.GetAllOrdersAsync();

        foreach (var order in orders)
        {
            if (order.LiquidatedAt == DateTime.Now.Date)
            {
                if (order.Direction == "Buy")
                {
                    await ExecuteBuyOrderAsync(order.Product.Id, order.Product.Id, order.NetValue).ConfigureAwait(false);
                }
                await ExecuteSellOrderAsync(order.Product.Id, order.Product.Id, order.Quotes, order.NetValue).ConfigureAwait(false);
            }
        }
    }

    public async Task ExecuteSellOrderAsync(long portfolioId, long productId, int quotes, decimal amount)
    {
        await _portfolioService.RedeemToPortfolioAsync(portfolioId, amount).ConfigureAwait(false);

        var orders = await _orderAppService.GetOrdersByPorfolioIdAndProductIdAsync(portfolioId, productId).ConfigureAwait(false);
        var sellingQuotes = quotes;
        var boughtQuotes = 0;

        foreach (var order in orders)
        {
            if (order.Direction == "Buy")
            {
                boughtQuotes += order.Quotes;
            }
            else
            {
                sellingQuotes += order.Quotes;
            }
        }

        foreach (var order in orders)
            if (boughtQuotes >= sellingQuotes)
                if (order.Direction == "Buy")
                {
                    await _portfolioProductAppService.DeleteAsync(portfolioId, productId).ConfigureAwait(false);
                    boughtQuotes -= order.Quotes;
                }
    }

    public async Task<IEnumerable<PortfolioResult>> GetAllPortfoliosAsync()
    {
        var portfolios = await _portfolioService.GetAllPortfoliosAsync().ConfigureAwait(false);

        return _mapper.Map<IEnumerable<PortfolioResult>>(portfolios);
    }

    public async Task<PortfolioResult> GetPortfolioByIdAsync(long portfolioId)
    {
        var portfolio = await _portfolioService.GetPortfolioByIdAsync(portfolioId).ConfigureAwait(false);

        return _mapper.Map<PortfolioResult>(portfolio);
    }

    public async Task<IEnumerable<PortfolioResult>> GetPortfoliosByCustomerIdAsync(long customerId)
    {
        var portfolios = await _portfolioService.GetPortfoliosByCustomerIdAsync(customerId).ConfigureAwait(false);

        return _mapper.Map<IEnumerable<PortfolioResult>>(portfolios);
    }

    public async Task InvestAsync(CreateOrder createOrderDto, decimal amount)
    {
        var product = await _productAppService.GetProductByIdAsync(createOrderDto.ProductId).ConfigureAwait(false);

        if (product != null)
        {
            createOrderDto.Direction = OrderDirection.Buy;
            await _orderAppService.CreateAsync(createOrderDto).ConfigureAwait(false);
        }

        if (createOrderDto.LiquidatedAt < DateTime.Now.Date)
            await ExecuteBuyOrderAsync(createOrderDto.ProductId, createOrderDto.ProductId, amount);
    }

    public async Task RedeemToPortfolioAsync(CreateOrder createOrderDto, decimal amount)
    {
        createOrderDto.Direction = OrderDirection.Sell;
        await _orderAppService.CreateAsync(createOrderDto).ConfigureAwait(false);

        if (createOrderDto.LiquidatedAt < DateTime.Now.Date)
            await ExecuteSellOrderAsync(createOrderDto.ProductId, createOrderDto.ProductId, createOrderDto.Quotes, amount);
    }

    public async Task<bool> WithdrawFromPortfolioAsync(long customerId, long portfolioId, decimal amount)
    {
        var result = await _portfolioService.WithdrawFromPortfolioAsync(portfolioId, amount).ConfigureAwait(false); ;
        await _customerBankInfoAppService.DepositAsync(customerId, amount).ConfigureAwait(false); ;

        return result;
    }
}