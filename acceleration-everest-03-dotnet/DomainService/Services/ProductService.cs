using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryFactory _repositoryFactory;

    public ProductService(
        IUnitOfWork<WarrenEverestDotnetDbContext> unitOfWork,
        IRepositoryFactory<WarrenEverestDotnetDbContext> repositoryFactory)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _repositoryFactory = repositoryFactory ?? (IRepositoryFactory)_unitOfWork;
    }

    public long Create(Product productToCreate)
    {
        var repository = _unitOfWork.Repository<Product>();
        repository.Add(productToCreate);
        _unitOfWork.SaveChanges();

        return productToCreate.Id;
    }

    public async Task DeleteAsync(long id)
    {
        var product = await GetProductByIdAsync(id).ConfigureAwait(false);
        var repository = _unitOfWork.Repository<Product>();
        repository.Remove(product);
        _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var repository = _repositoryFactory.Repository<Product>();
        var query = repository.MultipleResultQuery()
            .Include(product => product.Include(order => order.Orders)
            .Include(portfolio => portfolio.Portfolios)
            .Include(portfolioProduct => portfolioProduct.PortfolioProducts));
        var products = await repository.SearchAsync(query).ConfigureAwait(false);

        if (products.Count == 0)
            throw new ArgumentNullException($"No product found");

        return products;
    }

    public async Task<Product> GetProductByIdAsync(long id)
    {
        var repository = _repositoryFactory.Repository<Product>();
        var query = repository.SingleResultQuery().AndFilter(product => product.Id == id)
            .Include(product => product.Include(order => order.Orders)
            .Include(portfolio => portfolio.Portfolios)
            .Include(portfolioProduct => portfolioProduct.PortfolioProducts));
        var product = await repository.SingleOrDefaultAsync(query).ConfigureAwait(false);

        if (product == null)
            throw new ArgumentNullException($"No product found for Id: {id}");

        return product;
    }

    public async Task<decimal> GetProductUnitPriceByIdAsync (long id)
    {
        var repository = _repositoryFactory.Repository<Product>();
        var query = repository.SingleResultQuery().AndFilter(product => product.Id == id);
        var product = await repository.SingleOrDefaultAsync(query).ConfigureAwait(false);

        if (product == null)
            throw new ArgumentNullException($"No product found for Id: {id}");

        return product.UnitPrice;
    }

    public void Update(Product productToUpdate)
    {
        var repository = _unitOfWork.Repository<Product>();
        repository.Update(productToUpdate);
        _unitOfWork.SaveChanges();
    }
}