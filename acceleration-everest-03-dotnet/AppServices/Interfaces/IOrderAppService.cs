using AppModels.Orders;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface IOrderAppService
{
    long Create(CreateOrder createOrderDto);
    IEnumerable<OrderResult> GetAllOrders();
    OrderResult GetOrderById(long orderId);
    IEnumerable<OrderResult> GetOrdersByPorfolioIdAndProductId(long portfolioId, long productId);
    IEnumerable<OrderResult> GetOrdersByPortfolioId(long portfolioId);
    IEnumerable<OrderResult> GetOrdersByProductId(long productId);
}