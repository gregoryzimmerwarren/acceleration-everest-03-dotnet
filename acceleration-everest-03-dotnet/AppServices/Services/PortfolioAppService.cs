using AppModels.Enums;
using AppModels.Orders;
using AppModels.Portfolios;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Services;

public class PortfolioAppService : IPortfolioAppService
{
    private readonly ICustomerBankInfoAppService _customerBankInfoAppService;
    private readonly IPortfolioProductService _portfolioProductService;
    private readonly IProductAppService _productAppService;
    private readonly IPortfolioService _portfolioService;
    private readonly IOrderAppService _orderAppService;
    private readonly IMapper _mapper;

    public PortfolioAppService(
        ICustomerBankInfoAppService customerBankInfoService,
        IPortfolioProductService portfolioProductService,
        IProductAppService productAppService,
        IPortfolioService portfolioService,
        IOrderAppService orderAppService,
        IMapper mapper)
    {
        _customerBankInfoAppService = customerBankInfoService ?? throw new System.ArgumentNullException(nameof(customerBankInfoService));
        _portfolioProductService = portfolioProductService ?? throw new System.ArgumentNullException(nameof(portfolioProductService));
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
        var totalInBankInfo = await _customerBankInfoAppService.GetAccountBalanceByCustomerIdAsync(customerId).ConfigureAwait(false);

        if (totalInBankInfo < amount)
            throw new ArgumentException($"The customer bank info does not enough value to make this deposit. Current value: {totalInBankInfo}");

        await _customerBankInfoAppService.WithdrawAsync(customerId, amount).ConfigureAwait(false);
        await _portfolioService.DepositAsync(portfolioId, amount).ConfigureAwait(false);
    }

    public async Task ExecuteBuyOrderAsync(long portfolioId, long productId, decimal amount)
    {
        await _portfolioService.InvestAsync(portfolioId, amount).ConfigureAwait(false);

        try
        {
            await _portfolioProductService.GetPortfolioProductByIdsAsync(portfolioId, productId);
        }
        catch (ArgumentNullException)
        {
            _portfolioProductService.Create(new PortfolioProduct(portfolioId, productId));

        }
    }

    public async Task ExecuteOrdersOfTheDayAsync()
    {
        var orders = await _orderAppService.GetAllOrdersAsync();

        foreach (var order in orders)
        {
            if (order.LiquidatedAt.Date == DateTime.Now.Date && order.WasExecuted == false)
            {
                if (order.Direction == "Buy")
                {
                    await ExecuteBuyOrderAsync(order.Product.Id, order.Product.Id, order.NetValue).ConfigureAwait(false);
                }
                else
                {
                    await ExecuteSellOrderAsync(order.Product.Id, order.Product.Id, order.NetValue).ConfigureAwait(false);
                }

                _orderAppService.Update(new UpdateOrder(order.Id, order.Quotes, order.UnitPrice, order.Direction, order.WasExecuted, order.LiquidatedAt, order.Portfolio.Id, order.Product.Id));
            }
        }
    }

    public async Task ExecuteSellOrderAsync(long portfolioId, long productId, decimal amount)
    {
        await _portfolioService.RedeemToPortfolioAsync(portfolioId, amount).ConfigureAwait(false);

        var totalQuotes = await _orderAppService.GetAvailableQuotes(portfolioId, productId).ConfigureAwait(false);

        if (totalQuotes == 0)
            await _portfolioProductService.DeleteAsync(portfolioId, productId).ConfigureAwait(false);
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

    public async Task InvestAsync(CreateOrder createOrderDto)
    {
        var unitPrice = await _productAppService.GetProductUnitPriceByIdAsync(createOrderDto.ProductId);
        createOrderDto.UnitPrice = unitPrice;
        decimal amount = createOrderDto.UnitPrice * createOrderDto.Quotes;
        createOrderDto.NetValue = amount;
        createOrderDto.Direction = OrderDirection.Buy;

        if (createOrderDto.LiquidatedAt <= DateTime.Now.Date)
        {
            await ExecuteBuyOrderAsync(createOrderDto.PortfolioId, createOrderDto.ProductId, amount);
            createOrderDto.WasExecuted = true;
        }

        _orderAppService.Create(createOrderDto);
    }

    public async Task RedeemToPortfolioAsync(CreateOrder createOrderDto)
    {
        var unitPrice = await _productAppService.GetProductUnitPriceByIdAsync(createOrderDto.ProductId);
        createOrderDto.UnitPrice = unitPrice;
        decimal amount = createOrderDto.UnitPrice * createOrderDto.Quotes;
        createOrderDto.NetValue = amount;
        createOrderDto.Direction = OrderDirection.Sell;

        if (createOrderDto.LiquidatedAt <= DateTime.Now.Date)
        {
            await ExecuteSellOrderAsync(createOrderDto.PortfolioId, createOrderDto.ProductId, amount);
            createOrderDto.WasExecuted = true;
        }

        _orderAppService.Create(createOrderDto);
    }

    public async Task WithdrawFromPortfolioAsync(long customerId, long portfolioId, decimal amount)
    {
        await _portfolioService.WithdrawFromPortfolioAsync(portfolioId, amount).ConfigureAwait(false); ;
        await _customerBankInfoAppService.DepositAsync(customerId, amount).ConfigureAwait(false); ;
    }
}