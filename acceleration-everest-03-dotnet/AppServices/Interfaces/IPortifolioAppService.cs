using AppModels.Portfolios;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface IPortifolioAppService
{
    long Create(CreatePortfolioDto createPortfolioDto);
    void Delete(long portfolioId);
    void Deposit(long portfolioId, decimal amount);
    IEnumerable<PortfolioResultDto> GetAllPortifolios();
    PortfolioResultDto GetPortifolioById(long portfolioId);
    IEnumerable<PortfolioResultDto> GetPortifoliosByCustomerId(long customerId);
    bool Invest(long portfolioId, decimal amount);
    bool RedeemToPortfolio(long portfolioId, decimal amount);
    bool WithdrawFromPortfolio(long portfolioId, decimal amount);
}