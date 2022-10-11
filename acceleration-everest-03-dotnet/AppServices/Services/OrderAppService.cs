using AppModels.Orders;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System.Collections.Generic;

namespace AppServices.Services;

public class OrderAppService : IOrderAppService
{
    private readonly IPortfolioService _portfolioService;
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderAppService(
        IPortfolioService portfolioService, 
        IProductService productService, 
        IOrderService orderService, 
        IMapper mapper)
    {
        _portfolioService = portfolioService ?? throw new System.ArgumentNullException(nameof(portfolioService));
        _productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        _orderService = orderService ?? throw new System.ArgumentNullException(nameof(orderService));
        _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    public long Create(CreateOrderDto createOrderDto)
    {
        var orderMapeada = _mapper.Map<Order>(createOrderDto);

        return _orderService.Create(orderMapeada);
    }

    public IEnumerable<OrderResultDto> GetAllOrders()
    {
        var orders = _orderService.GetAllOrders();

        foreach (Order order in orders)
        {
            var portfolio = _portfolioService.GetPortfolioById(order.PortfolioId);
            order.Portfolio = _mapper.Map<Portfolio>(portfolio);

            var product = _productService.GetProductById(order.ProductId);
            order.Product = _mapper.Map<Product>(product);
        }

        return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
    }

    public OrderResultDto GetOrderById(long orderId)
    {
        var order = _orderService.GetOrderById(orderId);

        var portfolio = _portfolioService.GetPortfolioById(order.PortfolioId);
        order.Portfolio = _mapper.Map<Portfolio>(portfolio);

        var product = _productService.GetProductById(order.ProductId);
        order.Product = _mapper.Map<Product>(product);

        return _mapper.Map<OrderResultDto>(order);
    }

    public OrderResultDto GetOrderByPorfolioIdAndProductId(long portfolioId, long productId)
    {
        var order = _orderService.GetOrderByPorfolioIdAndProductId(portfolioId, productId);

        var portfolio = _portfolioService.GetPortfolioById(portfolioId);
        order.Portfolio = _mapper.Map<Portfolio>(portfolio);

        var product = _productService.GetProductById(productId);
        order.Product = _mapper.Map<Product>(product);

        return _mapper.Map<OrderResultDto>(order);
    }

    public IEnumerable<OrderResultDto> GetOrdersByPortfolioId(long portfolioId)
    {
        var orders = _orderService.GetOrdersByPortfolioId(portfolioId);

        foreach (Order order in orders)
        {
            var portfolio = _portfolioService.GetPortfolioById(order.PortfolioId);
            order.Portfolio = _mapper.Map<Portfolio>(portfolio);

            var product = _productService.GetProductById(order.ProductId);
            order.Product = _mapper.Map<Product>(product);
        }

        return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
    }

    public IEnumerable<OrderResultDto> GetOrdersByProductId(long productId)
    {
        var orders = _orderService.GetOrdersByProductId(productId);

        foreach (Order order in orders)
        {
            var portfolio = _portfolioService.GetPortfolioById(order.PortfolioId);
            order.Portfolio = _mapper.Map<Portfolio>(portfolio);

            var product = _productService.GetProductById(order.ProductId);
            order.Product = _mapper.Map<Product>(product);
        }

        return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
    }
}