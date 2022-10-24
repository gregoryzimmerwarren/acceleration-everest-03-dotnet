using DomainModels.Enums;
using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryFactory _repositoryFactory;

    public OrderService(
        IUnitOfWork<WarrenEverestDotnetDbContext> unitOfWork, 
        IRepositoryFactory<WarrenEverestDotnetDbContext> repositoryFactory)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _repositoryFactory = repositoryFactory ?? (IRepositoryFactory)_unitOfWork;
    }

    public long Create(Order orderToCreate)
    {
        var repository = _unitOfWork.Repository<Order>();
        repository.Add(orderToCreate);
        _unitOfWork.SaveChanges();

        return orderToCreate.Id;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        var repository = _repositoryFactory.Repository<Order>();
        var query = repository.MultipleResultQuery()
            .Include(order => order.Include(portfolio => portfolio.Portfolio)
            .Include(product => product.Product));
        var orders = await repository.SearchAsync(query).ConfigureAwait(false);

        if (orders.Count == 0)
            throw new ArgumentNullException("No order found");

        return orders;
    }

    public async Task<int> GetAvailableQuotes(long portfolioId, long productId)
    {
        var orders = await GetOrderByPorfolioIdAndProductIdAsync(portfolioId, productId).ConfigureAwait(false);

        var sellingQuotes = 0;
        var boughtQuotes = 0;

        foreach (var order in orders)
        {
            if (order.Direction == OrderDirection.Buy)
            {
                boughtQuotes += order.Quotes;
            }
            else
            {
                sellingQuotes += order.Quotes;
            }
        }

        var totalQuotes = boughtQuotes - sellingQuotes;
        return totalQuotes;
    }

    public async Task<Order> GetOrderByIdAsync(long orderId)
    {
        var repository = _repositoryFactory.Repository<Order>();
        var query = repository.SingleResultQuery().AndFilter(order => order.Id == orderId)
            .Include(order => order.Include(portfolio => portfolio.Portfolio)
            .Include(product => product.Product));
        var order = await repository.SingleOrDefaultAsync(query).ConfigureAwait(false);

        if (order == null)
            throw new ArgumentNullException($"No order found for Id: {orderId}");

        return order;
    }

    public async Task<IEnumerable<Order>> GetOrderByPorfolioIdAndProductIdAsync(long portfolioId, long productId)
    {
        var repository = _repositoryFactory.Repository<Order>();
        var query = repository.MultipleResultQuery().AndFilter(order => order.PortfolioId == portfolioId && order.ProductId == productId)
            .Include(order => order.Include(portfolio => portfolio.Portfolio)
            .Include(product => product.Product));
        var orders = await repository.SearchAsync(query).ConfigureAwait(false);

        if (orders.Count == 0)
            throw new ArgumentNullException($"No order was found between portfolio Id: {portfolioId} and product Id: {productId}");

        return orders;
    }

    public async Task<IEnumerable<Order>> GetOrdersByPortfolioIdAsync(long portfolioId)
    {
        var repository = _repositoryFactory.Repository<Order>();
        var query = repository.MultipleResultQuery().AndFilter(order => order.PortfolioId == portfolioId)
            .Include(order => order.Include(portfolio => portfolio.Portfolio)
            .Include(product => product.Product));
        var orders = await repository.SearchAsync(query).ConfigureAwait(false);

        if (orders.Count == 0)
            throw new ArgumentNullException($"No order found for portfolio Id: {portfolioId}");

        return orders;
    }

    public async Task<IEnumerable<Order>> GetOrdersByProductIdAsync(long productId)
    {
        var repository = _repositoryFactory.Repository<Order>();
        var query = repository.MultipleResultQuery().AndFilter(order => order.ProductId == productId)
            .Include(order => order.Include(portfolio => portfolio.Portfolio)
            .Include(product => product.Product));
        var orders = await repository.SearchAsync(query).ConfigureAwait(false);

        if (orders.Count == 0)
            throw new ArgumentNullException($"No order found for product Id: {productId}");

        return orders;
    }

    public void Update(Order orderToUpdate)
    {
        var repository = _unitOfWork.Repository<Order>();
        repository.Update(orderToUpdate);
        _unitOfWork.SaveChanges();
    }
}