using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace DomainServices.Services;

public class PortfolioService : IPortfolioService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryFactory _repositoryFactory;

    public PortfolioService(
        IUnitOfWork<WarrenEverestDotnetDbContext> unitOfWork,
        IRepositoryFactory<WarrenEverestDotnetDbContext> repositoryFactory)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _repositoryFactory = repositoryFactory ?? (IRepositoryFactory)_unitOfWork;
    }

    public long Create(Portfolio portfolioToCreate)
    {
        var repository = _unitOfWork.Repository<Portfolio>();
        repository.Add(portfolioToCreate);
        _unitOfWork.SaveChanges();

        return portfolioToCreate.Id;
    }

    public void Delete(long portfolioId)
    {
        var portfolio = GetPortfolioById(portfolioId);

        if (portfolio.TotalBalance > 0 || portfolio.AccountBalance > 0)
            throw new ArgumentException($@"It is not possible to delete the portfolio for Id: {portfolioId}.
Value available for redeem: R${portfolio.TotalBalance}.
Value available for withdraw: R${portfolio.AccountBalance}.");

        var repository = _unitOfWork.Repository<Portfolio>();
        repository.Remove(portfolio);
    }

    public void Deposit(long portfolioId, decimal amount)
    {
        var portfolio = GetPortfolioById(portfolioId);
        var newAccountBalance = portfolio.AccountBalance + amount;
        portfolio.AccountBalance = newAccountBalance;

        var repository = _unitOfWork.Repository<Portfolio>();
        repository.Update(portfolio);
        _unitOfWork.SaveChanges();
    }

    public IEnumerable<Portfolio> GetAllPortfolios()
    {
        var repository = _repositoryFactory.Repository<Portfolio>();
        var query = repository.MultipleResultQuery();
        var portfolios = repository.Search(query);

        if (portfolios.Count == 0)
            throw new ArgumentException("No portfolio found");

        return portfolios;
    }

    public Portfolio GetPortfolioById(long portfolioId)
    {
        var repository = _repositoryFactory.Repository<Portfolio>();
        var query = repository.SingleResultQuery().AndFilter(portfolio => portfolio.Id == portfolioId);
        var portfolio = repository.SingleOrDefault(query);

        if (portfolio == null)
            throw new ArgumentException($"No portfolio found for Id: {portfolioId}");

        return portfolio;

    }

    public IEnumerable<Portfolio> GetPortfoliosByCustomerId(long customerId)
    {
        var repository = _repositoryFactory.Repository<Portfolio>();
        var query = repository.MultipleResultQuery().AndFilter(portfolio => portfolio.CustomerId == customerId);
        var portfolios = repository.Search(query);

        //if (portfolios.Count == 0)
        //    throw new ArgumentException($"No portfolio found for customer Id: {customerId}");

        return portfolios;
    }

    public bool Invest(long portfolioId, decimal amount)
    {
        var portfolio = GetPortfolioById(portfolioId);

        if (portfolio.AccountBalance < amount)
            throw new ArgumentException($"Portfolio does not have sufficient balance for this investment. Current balance: R${portfolio.AccountBalance}");

        var newAccountBalance = portfolio.AccountBalance - amount;
        var newTotalBalance = portfolio.TotalBalance + amount;
        portfolio.AccountBalance = newAccountBalance;
        portfolio.TotalBalance = newTotalBalance;

        var repository = _unitOfWork.Repository<Portfolio>();
        repository.Update(portfolio);
        _unitOfWork.SaveChanges();

        return true;
    }

    public bool RedeemToPortfolio(long portfolioId, decimal amount)
    {
        var portfolio = GetPortfolioById(portfolioId);

        if (portfolio.TotalBalance < amount)
            throw new ArgumentException($"Portfolio does not have sufficient balance for this redeem. Current balance: R${portfolio.TotalBalance}");

        var newTotalBalance = portfolio.TotalBalance - amount;
        var newAccountBalance = portfolio.AccountBalance + amount;
        portfolio.TotalBalance = newTotalBalance;
        portfolio.AccountBalance = newAccountBalance;

        var repository = _unitOfWork.Repository<Portfolio>();
        repository.Update(portfolio);
        _unitOfWork.SaveChanges();

        return true;
    }

    public bool WithdrawFromPortfolio(long portfolioId, decimal amount)
    {
        var portfolio = GetPortfolioById(portfolioId);

        if (portfolio.AccountBalance < amount)
            throw new ArgumentException($"Portfolio does not have sufficient balance for this withdraw. Current balance: R${portfolio.AccountBalance}");

        var newAccountBalance = portfolio.AccountBalance - amount;
        portfolio.AccountBalance = newAccountBalance;

        var repository = _unitOfWork.Repository<Portfolio>();
        repository.Update(portfolio);
        _unitOfWork.SaveChanges();

        return true;
    }
}