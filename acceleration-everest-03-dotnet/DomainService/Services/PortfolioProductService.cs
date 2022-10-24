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

    public void Create(PortfolioProduct portfolioProductToCreate)
    {
        var repository = _unitOfWork.Repository<PortfolioProduct>();
        repository.Add(portfolioProductToCreate);
        _unitOfWork.SaveChanges();
    }

    public async Task DeleteAsync(long portfolioId, long productId)
    {
        var portfolioProduct = await GetPortfolioProductByIdsAsync(portfolioId, productId).ConfigureAwait(false);
        var repository = _unitOfWork.Repository<PortfolioProduct>();
        repository.Remove(portfolioProduct);
        _unitOfWork.SaveChanges();
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
}