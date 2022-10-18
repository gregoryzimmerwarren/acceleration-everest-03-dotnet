using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Services;

public class PortfolioProductService : IPortfolioProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryFactory _repositoryFactory;

    public PortfolioProductService(
        IUnitOfWork<WarrenEverestDotnetDbContext> unitOfWork,
        IRepositoryFactory<WarrenEverestDotnetDbContext> repositoryFactory)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _repositoryFactory = repositoryFactory ?? (IRepositoryFactory)_unitOfWork;
    }

    public long Create(PortfolioProduct portfolioProductToCreate)
    {
        var repository = _unitOfWork.Repository<PortfolioProduct>();
        repository.Add(portfolioProductToCreate);
        _unitOfWork.SaveChanges();

        return portfolioProductToCreate.Id;
    }

    public async Task DeleteAsync(long portfolioId, long productId)
    {
        var portfolioProduct = await GetPortfolioProductByIdsAsync(portfolioId, productId).ConfigureAwait(false);
        var repository = _unitOfWork.Repository<PortfolioProduct>();
        repository.Remove(portfolioProduct);
        _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<PortfolioProduct>> GetAllPortfolioProductAsync()
    {
        var repository = _repositoryFactory.Repository<PortfolioProduct>();
        var query = repository.MultipleResultQuery()
            .Include(portfolioProduct => portfolioProduct.Include(portfolio => portfolio.Portfolio)
            .Include(product => product.Product));
        var portfoliosProducts = await repository.SearchAsync(query).ConfigureAwait(false);

        if (portfoliosProducts.Count == 0)
            throw new ArgumentNullException($"No relationship between portfolio and product found");

        return portfoliosProducts;
    }

    public async Task<PortfolioProduct> GetPortfolioProductByIdsAsync(long portfolioId, long productId)
    {
        var repository = _repositoryFactory.Repository<PortfolioProduct>();
        var query = repository.SingleResultQuery().AndFilter(portfolioProduct => portfolioProduct.PortfolioId == portfolioId
        && portfolioProduct.ProductId == productId)
            .Include(portfolioProduct => portfolioProduct.Include(portfolio => portfolio.Portfolio)
            .Include(product => product.Product));
        var portfolioProduct = await repository.FirstOrDefaultAsync(query).ConfigureAwait(false);

        if (portfolioProduct == null)
            throw new ArgumentNullException($"No relationship was found between portfolio Id: {portfolioId} and product Id: {productId}");

        return portfolioProduct;
    }

    public async Task<IEnumerable<PortfolioProduct>> GetPortfolioProductByProductIdAsync(long productId)
    {
        var repository = _repositoryFactory.Repository<PortfolioProduct>();
        var query = repository.MultipleResultQuery().AndFilter(portfolioProduct => portfolioProduct.ProductId == productId)
            .Include(portfolioProduct => portfolioProduct.Include(portfolio => portfolio.Portfolio)
            .Include(product => product.Product));
        var portfoliosProducts = await repository.SearchAsync(query).ConfigureAwait(false);

        if (portfoliosProducts.Count == 0)
            throw new ArgumentNullException($"No portfolio found for product Id: {productId}");

        return portfoliosProducts;
    }

    public async Task<IEnumerable<PortfolioProduct>> GetPortfolioProductByPortfolioIdAsync(long portfolioId)
    {
        var repository = _repositoryFactory.Repository<PortfolioProduct>();
        var query = repository.MultipleResultQuery().AndFilter(portfolioProduct => portfolioProduct.PortfolioId == portfolioId)
            .Include(portfolioProduct => portfolioProduct.Include(portfolio => portfolio.Portfolio)
            .Include(product => product.Product));
        var portfoliosProducts = await repository.SearchAsync(query).ConfigureAwait(false);

        if (portfoliosProducts.Count == 0)
            throw new ArgumentNullException($"No product found for portfolio Id: {portfolioId}");

        return portfoliosProducts;
    }
}