using AppModels.Orders;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Services;

public class OrderAppService : IOrderAppService
{
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderAppService(
        IProductService productService, 
        IOrderService orderService, 
        IMapper mapper)
    {
        _productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        _orderService = orderService ?? throw new System.ArgumentNullException(nameof(orderService));
        _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    public async Task<long> CreateAsync(CreateOrder createOrderDto)
    {
        var mappedOrder = _mapper.Map<Order>(createOrderDto);
        var produt = await _productService.GetProductByIdAsync(mappedOrder.ProductId).ConfigureAwait(false);
        var unitPrice = produt.UnitPrice;
        mappedOrder.NetValue = mappedOrder.Quotes * unitPrice;

        return _orderService.Create(mappedOrder);
    }

    public async Task<IEnumerable<OrderResult>> GetAllOrdersAsync()
    {
        var orders = await _orderService.GetAllOrdersAsync().ConfigureAwait(false);

        return _mapper.Map<IEnumerable<OrderResult>>(orders);
    }

    public async Task<OrderResult> GetOrderByIdAsync(long orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);

        return _mapper.Map<OrderResult>(order);
    }

    public async Task<IEnumerable<OrderResult>> GetOrdersByPorfolioIdAndProductIdAsync(long portfolioId, long productId)
    {
        var orders = await _orderService.GetOrderByPorfolioIdAndProductIdAsync(portfolioId, productId).ConfigureAwait(false);

        return _mapper.Map<IEnumerable<OrderResult>>(orders);
    }

    public async Task<IEnumerable<OrderResult>> GetOrdersByPortfolioIdAsync(long portfolioId)
    {
        var orders = await _orderService.GetOrdersByPortfolioIdAsync(portfolioId).ConfigureAwait(false);

        return _mapper.Map<IEnumerable<OrderResult>>(orders);
    }

    public async Task<IEnumerable<OrderResult>> GetOrdersByProductIdAsync(long productId)
    {
        var orders = await _orderService.GetOrdersByProductIdAsync(productId).ConfigureAwait(false);

        return _mapper.Map<IEnumerable<OrderResult>>(orders);
    }
}