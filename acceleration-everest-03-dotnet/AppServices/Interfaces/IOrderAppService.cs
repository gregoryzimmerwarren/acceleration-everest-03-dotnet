using AppModels.Orders;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface IOrderAppService
{
    long Create(CreateOrderDto createOrderDto);
    IEnumerable<OrderResultDto> GetAllOrders();
    OrderResultDto GetOrderById(long orderId);
    OrderResultDto GetOrderByPorfolioIdAndProductId(long portfolioId, long productId);
    IEnumerable<OrderResultDto> GetOrdersByPortfolioId(long portfolioId);
    IEnumerable<OrderResultDto> GetOrdersByProductId(long productId);
}