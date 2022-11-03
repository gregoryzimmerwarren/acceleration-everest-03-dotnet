using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainServices.Services;

public class PortfolioService : IPortfolioService
{
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioService(
        IRepositoryFactory<WarrenEverestDotnetDbContext> repositoryFactory,
        IUnitOfWork<WarrenEverestDotnetDbContext> unitOfWork)
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

    public async Task DeleteAsync(long portfolioId)
    {
        var portfolio = await GetPortfolioByIdAsync(portfolioId).ConfigureAwait(false);

        if (portfolio.TotalBalance > 0 || portfolio.AccountBalance > 0)
            throw new ArgumentException($@"It is not possible to delete the portfolio for Id: {portfolioId}.
Value available for redeem: R${portfolio.TotalBalance}.
Value available for withdraw: R${portfolio.AccountBalance}.");

        var repository = _unitOfWork.Repository<Portfolio>();
        repository.Remove(portfolio);
        _unitOfWork.SaveChanges();
    }

    public async Task DepositAsync(long portfolioId, decimal amount)
    {
        var portfolio = await GetPortfolioByIdAsync(portfolioId).ConfigureAwait(false);
        portfolio.AccountBalance += amount;

        var repository = _unitOfWork.Repository<Portfolio>();
        repository.Update(portfolio);
        _unitOfWork.SaveChanges();
    }

    public async Task<Portfolio> GetPortfolioByIdAsync(long portfolioId)
    {
        var repository = _repositoryFactory.Repository<Portfolio>();
        var query = repository.SingleResultQuery().AndFilter(portfolio => portfolio.Id == portfolioId)
            .Include(portfolio => portfolio.Include(portfolio => portfolio.Customer)
            .Include(portfolio => portfolio.Orders)
            .ThenInclude(order => order.Product)
            .Include(portfolio => portfolio.Products));
        var portfolio = await repository.SingleOrDefaultAsync(query).ConfigureAwait(false)
            ?? throw new ArgumentNullException($"No portfolio found for Id: {portfolioId}");

        return portfolio;
    }

    public async Task<IEnumerable<Portfolio>> GetPortfoliosByCustomerIdAsync(long customerId)
    {
        var repository = _repositoryFactory.Repository<Portfolio>();
        var query = repository.MultipleResultQuery().AndFilter(portfolio => portfolio.CustomerId == customerId)
            .Include(portfolio => portfolio.Include(portfolio => portfolio.Customer)
            .Include(portfolio => portfolio.Orders)
            .ThenInclude(order => order.Product)
            .Include(product => product.Products));
        var portfolios = await repository.SearchAsync(query).ConfigureAwait(false);

        if (!portfolios.Any())
            throw new ArgumentNullException($"No portfolio found for Customer with Id: {customerId}");

        return portfolios;
    }

    public async Task InvestAsync(long portfolioId, decimal amount)
    {
        var portfolio = await GetPortfolioByIdAsync(portfolioId).ConfigureAwait(false);

        if (portfolio.AccountBalance < amount)
            throw new ArgumentException($"Portfolio does not have sufficient balance for this investment. Current balance: R${portfolio.AccountBalance}");

        portfolio.AccountBalance -= amount;
        portfolio.TotalBalance += amount;

        var repository = _unitOfWork.Repository<Portfolio>();
        repository.Update(portfolio);
        _unitOfWork.SaveChanges();
    }

    public async Task RedeemToPortfolioAsync(long portfolioId, decimal amount)
    {
        var portfolio = await GetPortfolioByIdAsync(portfolioId).ConfigureAwait(false);

        if (portfolio.TotalBalance < amount)
            throw new ArgumentException($"Portfolio does not have sufficient balance for this redeem. Current balance: R${portfolio.TotalBalance}");

        portfolio.TotalBalance -= amount;
        portfolio.AccountBalance += amount;

        var repository = _unitOfWork.Repository<Portfolio>();
        repository.Update(portfolio);
        _unitOfWork.SaveChanges();
    }

    public async Task WithdrawFromPortfolioAsync(long portfolioId, decimal amount)
    {
        var portfolio = await GetPortfolioByIdAsync(portfolioId).ConfigureAwait(false);

        if (portfolio.AccountBalance < amount)
            throw new ArgumentException($"Portfolio does not have sufficient balance for this withdraw. Current balance: R${portfolio.AccountBalance}");

        portfolio.AccountBalance -= amount;

        var repository = _unitOfWork.Repository<Portfolio>();
        repository.Update(portfolio);
        _unitOfWork.SaveChanges();
    }
}