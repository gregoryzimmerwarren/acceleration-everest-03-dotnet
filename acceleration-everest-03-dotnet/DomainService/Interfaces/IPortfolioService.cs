using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces;

public interface IPortfolioService
{
    long Create(Portfolio portfolioToCreate);
    Task DeleteAsync(long portfolioId);
    Task DepositAsync(long portfolioId, decimal amount);
    Task<Portfolio> GetPortfolioByIdAsync(long portfolioId);
    Task<IEnumerable<Portfolio>> GetPortfoliosByCustomerIdAsync(long customerId);
    Task InvestAsync(long portfolioId, decimal amount);
    Task RedeemToPortfolioAsync(long portfolioId, decimal amount);
    Task WithdrawFromPortfolioAsync(long portfolioId, decimal amount);
}