using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces;

public interface IOrderService
{
    long Create(Order orderToCreate);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(long orderId);
    Task<IEnumerable<Order>> GetOrderByPorfolioIdAndProductIdAsync(long portfolioId, long productId);
    Task<IEnumerable<Order>> GetOrdersByPortfolioIdAsync(long portfolioId);
    Task<IEnumerable<Order>> GetOrdersByProductIdAsync(long productId);
    void Update(Order orderToUpdate);
}