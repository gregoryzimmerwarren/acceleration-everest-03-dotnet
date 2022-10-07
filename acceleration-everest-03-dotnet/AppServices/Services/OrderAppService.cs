using AppModels.Orders;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System.Collections.Generic;

namespace AppServices.Services;

public class OrderAppService : IOrderAppService
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderAppService(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    public long Create(CreateOrderDto createOrderDto)
    {
        var orderMapeada = _mapper.Map<Order>(createOrderDto);

        return _orderService.Create(orderMapeada);
    }

    public IEnumerable<OrderResultDto> GetAllOrders()
    {
        var orders = _orderService.GetAllOrders();

        return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
    }

    public OrderResultDto GetOrderById(long orderId)
    {
        var order = _orderService.GetOrderById(orderId);

        return _mapper.Map<OrderResultDto>(order);
    }

    public IEnumerable<OrderResultDto> GetOrdersByPortfolioId(long portfolioId)
    {
        var orders = _orderService.GetOrdersByPortfolioId(portfolioId);

        return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
    }

    public IEnumerable<OrderResultDto> GetOrdersByProductId(long productId)
    {
        var orders = _orderService.GetOrdersByProductId(productId);

        return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
    }
}