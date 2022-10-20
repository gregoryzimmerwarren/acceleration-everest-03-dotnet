using AppModels.Orders;
using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces;

public interface IOrderAppService
{
    Task<long> CreateAsync(CreateOrder createOrderDto);
    Task<IEnumerable<OrderResult>> GetAllOrdersAsync();
    Task<OrderResult> GetOrderByIdAsync(long orderId);
    Task<int> GetAvailableQuotes(long portfolioId, long productId);
    Task<IEnumerable<OrderResult>> GetOrdersByPortfolioIdAsync(long portfolioId);
    Task<IEnumerable<OrderResult>> GetOrdersByProductIdAsync(long productId);
    void Update(UpdateOrder updateOrderDto);
}