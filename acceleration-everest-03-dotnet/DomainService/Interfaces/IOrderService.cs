using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Interfaces;

public interface IOrderService
{
    long Create(Order orderToCreate);
    IEnumerable<Order> GetAllOrders();
    Order GetOrderById(long orderId);
    IEnumerable<Order> GetOrderByPorfolioIdAndProductId(long portfolioId, long productId);
    IEnumerable<Order> GetOrdersByPortfolioId(long portfolioId);
    IEnumerable<Order> GetOrdersByProductId(long productId);
}