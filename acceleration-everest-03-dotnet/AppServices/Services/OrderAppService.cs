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

    public long Create(CreateOrder createOrderDto)
    {
        var mappedOrder = _mapper.Map<Order>(createOrderDto);
        var unitPrice = _productService.GetProductById(mappedOrder.ProductId).UnitPrice;
        mappedOrder.NetValue = mappedOrder.Quotes * unitPrice;

        return _orderService.Create(mappedOrder);
    }

    public IEnumerable<OrderResult> GetAllOrders()
    {
        var orders = _orderService.GetAllOrders();

        foreach (Order order in orders)
        {
            var portfolio = _portfolioService.GetPortfolioById(order.PortfolioId);
            order.Portfolio = _mapper.Map<Portfolio>(portfolio);

            var product = _productService.GetProductById(order.ProductId);
            order.Product = _mapper.Map<Product>(product);
        }

        return _mapper.Map<IEnumerable<OrderResult>>(orders);
    }

    public OrderResult GetOrderById(long orderId)
    {
        var order = _orderService.GetOrderById(orderId);

        var portfolio = _portfolioService.GetPortfolioById(order.PortfolioId);
        order.Portfolio = _mapper.Map<Portfolio>(portfolio);

        var product = _productService.GetProductById(order.ProductId);
        order.Product = _mapper.Map<Product>(product);

        return _mapper.Map<OrderResult>(order);
    }

    public IEnumerable<OrderResult> GetOrdersByPorfolioIdAndProductId(long portfolioId, long productId)
    {
        var orders = _orderService.GetOrderByPorfolioIdAndProductId(portfolioId, productId);

        foreach (Order order in orders)
        {
            var portfolio = _portfolioService.GetPortfolioById(order.PortfolioId);
            order.Portfolio = _mapper.Map<Portfolio>(portfolio);

            var product = _productService.GetProductById(order.ProductId);
            order.Product = _mapper.Map<Product>(product);
        }

        return _mapper.Map<IEnumerable<OrderResult>>(orders);
    }

    public IEnumerable<OrderResult> GetOrdersByPortfolioId(long portfolioId)
    {
        var orders = _orderService.GetOrdersByPortfolioId(portfolioId);

        foreach (Order order in orders)
        {
            var portfolio = _portfolioService.GetPortfolioById(order.PortfolioId);
            order.Portfolio = _mapper.Map<Portfolio>(portfolio);

            var product = _productService.GetProductById(order.ProductId);
            order.Product = _mapper.Map<Product>(product);
        }

        return _mapper.Map<IEnumerable<OrderResult>>(orders);
    }

    public IEnumerable<OrderResult> GetOrdersByProductId(long productId)
    {
        var orders = _orderService.GetOrdersByProductId(productId);

        foreach (Order order in orders)
        {
            var portfolio = _portfolioService.GetPortfolioById(order.PortfolioId);
            order.Portfolio = _mapper.Map<Portfolio>(portfolio);

            var product = _productService.GetProductById(order.ProductId);
            order.Product = _mapper.Map<Product>(product);
        }

        return _mapper.Map<IEnumerable<OrderResult>>(orders);
    }
}