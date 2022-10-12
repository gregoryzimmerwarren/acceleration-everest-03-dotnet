using AppModels.Orders;
using AppModels.Portfolios;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface IPortfolioAppService
{
    long Create(CreatePortfolioDto createPortfolioDto);
    void Delete(long portfolioId);
    string Deposit(long customerId, long portfolioId, decimal amount);
    IEnumerable<PortfolioResultDto> GetAllPortfolios();
    PortfolioResultDto GetPortfolioById(long portfolioId);
    IEnumerable<PortfolioResultDto> GetPortfoliosByCustomerId(long customerId);
    bool Invest(CreateOrderDto createOrderDto, decimal amount);
    bool RedeemToPortfolio(CreateOrderDto createOrderDto, decimal amount);
    bool WithdrawFromPortfolio(long customerId, long portfolioId, decimal amount);
}