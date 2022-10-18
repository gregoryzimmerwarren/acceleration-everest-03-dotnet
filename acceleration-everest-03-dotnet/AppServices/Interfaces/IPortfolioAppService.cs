using AppModels.Orders;
using AppModels.Portfolios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces;

public interface IPortfolioAppService
{
    long Create(CreatePortfolio createPortfolioDto);
    Task DeleteAsync(long portfolioId);
    Task DepositAsync(long customerId, long portfolioId, decimal amount);
    Task<IEnumerable<PortfolioResult>> GetAllPortfoliosAsync();
    Task<PortfolioResult> GetPortfolioByIdAsync(long portfolioId);
    Task<IEnumerable<PortfolioResult>> GetPortfoliosByCustomerIdAsync(long customerId);
    Task<bool> InvestAsync(CreateOrder createOrderDto, decimal amount);
    Task<bool> RedeemToPortfolioAsync(CreateOrder createOrderDto, decimal amount);
    Task<bool> WithdrawFromPortfolioAsync(long customerId, long portfolioId, decimal amount);
}