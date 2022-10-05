using AppModels.Portfolios;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System.Collections.Generic;

namespace AppServices.Services;

public class PortifolioAppService : IPortifolioAppService
{
    private readonly IPortfolioService _portfolioService;
    private readonly IMapper _mapper;

    public PortifolioAppService(IPortfolioService portfolioService, IMapper mapper)
    {
        _portfolioService = portfolioService;
        _mapper = mapper;
    }

    public long Create(CreatePortfolioDto createPortfolioDto)
    {
        var portfolioMapeado = _mapper.Map<Portfolio>(createPortfolioDto);

        return _portfolioService.Create(portfolioMapeado);
    }

    public void Delete(long portfolioId)
    {
        _portfolioService.Delete(portfolioId);
    }

    public void Deposit(long portfolioId, decimal amount)
    {
        _portfolioService.Deposit(portfolioId, amount);
    }

    public IEnumerable<PortfolioResultDto> GetAllPortifolios()
    {
        var portfolios = _portfolioService.GetAllPortifolios();

        return _mapper.Map<IEnumerable<PortfolioResultDto>>(portfolios);
    }

    public PortfolioResultDto GetPortifolioById(long portfolioId)
    {
        var portfolio = _portfolioService.GetPortifolioById(portfolioId);

        return _mapper.Map<PortfolioResultDto>(portfolio);
    }

    public IEnumerable<PortfolioResultDto> GetPortifoliosByCustomerId(long customerId)
    {
        var portfolios = _portfolioService.GetPortifoliosByCustomerId(customerId);

        return _mapper.Map<IEnumerable<PortfolioResultDto>>(portfolios);
    }

    public bool Invest(long portfolioId, decimal amount)
    {
        var result = _portfolioService.Invest(portfolioId, amount);

        return result;
    }

    public bool RedeemToPortfolio(long portfolioId, decimal amount)
    {
        var result = _portfolioService.RedeemToPortfolio(portfolioId, amount);

        return result;
    }

    public bool WithdrawFromPortfolio(long portfolioId, decimal amount)
    {
        var result = _portfolioService.WithdrawFromPortfolio(portfolioId, amount);

        return result;
    }
}