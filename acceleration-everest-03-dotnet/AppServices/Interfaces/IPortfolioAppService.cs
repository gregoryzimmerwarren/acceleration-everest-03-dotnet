using AppModels.Orders;
using AppModels.Portfolios;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface IPortfolioAppService
{
    long Create(CreatePortfolioDto createPortfolioDto);
    void Delete(long portfolioId);
    void Deposit(long customerId, long portfolioId, decimal amount, bool amountInBankInfo);
    IEnumerable<PortfolioResultDto> GetAllPortfolios();
    PortfolioResultDto GetPortfolioById(long portfolioId);
    IEnumerable<PortfolioResultDto> GetPortfoliosByCustomerId(long customerId);
    bool Invest(CreateOrderDto createOrderDto, long portfolioId, long productId, decimal amount);
    bool RedeemToPortfolio(CreateOrderDto createOrderDto, long portfolioId, long productId, decimal amount);
    bool WithdrawFromPortfolio(long customerId, long portfolioId, decimal amount);
}