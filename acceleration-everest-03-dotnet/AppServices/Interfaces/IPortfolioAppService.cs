using AppModels.Orders;
using AppModels.Portfolios;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface IPortfolioAppService
{
    long Create(CreatePortfolio createPortfolioDto);
    void Delete(long portfolioId);
    string Deposit(long customerId, long portfolioId, decimal amount);
    IEnumerable<PortfolioResult> GetAllPortfolios();
    PortfolioResult GetPortfolioById(long portfolioId);
    IEnumerable<PortfolioResult> GetPortfoliosByCustomerId(long customerId);
    bool Invest(CreateOrder createOrderDto, decimal amount);
    bool RedeemToPortfolio(CreateOrder createOrderDto, decimal amount);
    bool WithdrawFromPortfolio(long customerId, long portfolioId, decimal amount);
}