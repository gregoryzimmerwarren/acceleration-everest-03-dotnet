﻿using AppModels.Orders;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface IOrderAppService
{
    long Create(CreateOrderDto createOrderDto);
    IEnumerable<OrderResultDto> GetAllOrders();
    OrderResultDto GetOrderById(long orderId);
    IEnumerable<OrderResultDto> GetOrdersByPortifolioId(long portifolioId);
    IEnumerable<OrderResultDto> GetOrdersByProductId(long productId);
}