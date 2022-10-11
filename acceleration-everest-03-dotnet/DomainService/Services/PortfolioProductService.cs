using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;

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

    public void Delete(long portfolioId, long productId)
    {
        var portfolioProduct = GetPortfolioProductByIds(portfolioId, productId);
        var repository = _unitOfWork.Repository<PortfolioProduct>();
        repository.Remove(portfolioProduct);
        _unitOfWork.SaveChanges();
    }

    public IEnumerable<PortfolioProduct> GetAllPortfolioProduct()
    {
        var repository = _repositoryFactory.Repository<PortfolioProduct>();
        var query = repository.MultipleResultQuery();
        var portfoliosProducts = repository.Search(query);

        if (portfoliosProducts.Count == 0)
            throw new ArgumentException($"No relationship between portfolio and product found");

        return portfoliosProducts;
    }

    public PortfolioProduct GetPortfolioProductByIds(long portfolioId, long productId)
    {
        var repository = _repositoryFactory.Repository<PortfolioProduct>();
        var query = repository.SingleResultQuery().AndFilter(portfolioProduct => portfolioProduct.PortfolioId == portfolioId
        && portfolioProduct.ProductId == productId);
        var portfolioProduct = repository.FirstOrDefault(query);

        if (portfolioProduct == null)
            throw new ArgumentException($"No relationship was found between portfolio Id: {portfolioId} and product Id: {productId}");

        return portfolioProduct;
    }

    public IEnumerable<PortfolioProduct> GetPortfolioProductByProductId(long productId)
    {
        var repository = _repositoryFactory.Repository<PortfolioProduct>();
        var query = repository.MultipleResultQuery().AndFilter(portfolioProduct => portfolioProduct.ProductId == productId);
        var portfoliosProducts = repository.Search(query);

        if (portfoliosProducts.Count == 0)
            throw new ArgumentException($"No portfolio found for product Id: {productId}");

        return portfoliosProducts;
    }

    public IEnumerable<PortfolioProduct> GetPortfolioProductByPortfolioId(long portfolioId)
    {
        var repository = _repositoryFactory.Repository<PortfolioProduct>();
        var query = repository.MultipleResultQuery().AndFilter(portfolioProduct => portfolioProduct.PortfolioId == portfolioId);
        var portfoliosProducts = repository.Search(query);

        if (portfoliosProducts.Count == 0)
            throw new ArgumentException($"No product found for portfolio Id: {portfolioId}");

        return portfoliosProducts;
    }
}