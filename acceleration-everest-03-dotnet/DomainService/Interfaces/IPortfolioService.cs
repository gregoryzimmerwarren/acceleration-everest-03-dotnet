using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Interfaces;

public interface IPortfolioService
{
    long Create(Portfolio portfolioToCreate);
    void Delete(long portfolioId);
    void Deposit(long portfolioId, decimal amount);
    IEnumerable<Portfolio> GetAllPortfolios();
    Portfolio GetPortfolioById(long portfolioId);
    IEnumerable<Portfolio> GetPortfoliosByCustomerId(long customerId);
    bool Invest(long portfolioId, decimal amount);
    bool RedeemToPortfolio(long portfolioId, decimal amount);
    bool WithdrawFromPortfolio(long portfolioId, decimal amount);
}