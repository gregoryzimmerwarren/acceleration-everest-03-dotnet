using AppModels.Orders;
using AppModels.Products;
using AppServices.Interfaces;
using AppServices.Services;
using AppServices.Tests.Fixtures.Orders;
using AppServices.Tests.Fixtures.Portfolios;
using AppServices.Tests.Fixtures.Products;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using FluentAssertions;
using Moq;
using System.Collections.Generic;

namespace AppServices.Tests.Services;

public class OrderAppServiceTests
{
    private readonly Mock<IProductAppService> _mockProductAppService;
    private readonly Mock<IOrderService> _mockOrderService;
    private readonly OrderAppService _orderAppService;
    private readonly IMapper _mapper;

    public OrderAppServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ProductResult>();
            cfg.CreateMap<Order, OrderResult>();
            cfg.CreateMap<Order, OrderResultOtherDtos>();
            cfg.CreateMap<CreateOrder, Order>();
            cfg.CreateMap<UpdateOrder, Order>();
        });
        _mapper = config.CreateMapper();
        _mockOrderService = new Mock<IOrderService>();
        _mockProductAppService = new Mock<IProductAppService>();
        _orderAppService = new OrderAppService(_mockProductAppService.Object, _mockOrderService.Object, _mapper);
    }

    [Fact]
    public async void Should_CreateOrderAsync_Successfully()
    {
        // Arrange
        long idTest = 1;
        var createdOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        var orderTest = OrderFixture.GenerateOrderFixture();
        var productTest = ProductFixture.GenerateProductFixture();
        var productResultTest = ProductResultFixture.GenerateProductResultFixture();

        _mockProductAppService.Setup(productService => productService.GetProductByIdAsync(It.IsAny<long>())).ReturnsAsync(productResultTest);
        _mockOrderService.Setup(orderService => orderService.Create(It.IsAny<Order>())).Returns(idTest);

        // Action
        var resultOrder = await _orderAppService.CreateAsync(createdOrderTest).ConfigureAwait(false);

        // Assert
        resultOrder.Should().Be(idTest);
        _mockProductAppService.Verify(productService => productService.GetProductByIdAsync(It.IsAny<long>()), Times.Once);
        _mockOrderService.Verify(orderService => orderService.Create(It.IsAny<Order>()), Times.Once);
    }

    [Fact]
    public async void Should_GetAllOrdersAsync_Successfully()
    {
        // Arrange
        var listOrderResultTest = OrderResultFixture.GenerateListOrderResultFixture(3);
        var listOrderTest = OrderFixture.GenerateListOrderFixture(3);

        _mockOrderService.Setup(orderService => orderService.GetAllOrdersAsync()).ReturnsAsync(listOrderTest);
        _mapper.Map<IEnumerable<OrderResult>>(listOrderTest);

        // Action
        var result = await _orderAppService.GetAllOrdersAsync().ConfigureAwait(false);

        // Assert
        result.Should().HaveCountGreaterThanOrEqualTo(3);
        _mockOrderService.Verify(orderService => orderService.GetAllOrdersAsync(), Times.Once);
    }

    [Fact]
    public async void Should_GetAvailableQuotes_Successfully()
    {
        // Arrange
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        var productTest = ProductFixture.GenerateProductFixture();
        var listOrderTest = OrderFixture.GenerateListOrderFixture(3);

        _mockOrderService.Setup(orderService => orderService.GetOrderByPorfolioIdAndProductIdAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(listOrderTest);

        // Action
        var result = await _orderAppService.GetAvailableQuotes(portfolioTest.Id, productTest.Id).ConfigureAwait(false);

        // Assert
        result.Should().NotBe(null);
        _mockOrderService.Verify(orderService => orderService.GetOrderByPorfolioIdAndProductIdAsync(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_GetOrdersByPortfolioIdAsync_Successfully()
    {
        // Arrange
        var orderListTest = OrderFixture.GenerateListOrderFixture(3);
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();

        _mockOrderService.Setup(orderService => orderService.GetOrdersByPortfolioIdAsync(It.IsAny<long>())).ReturnsAsync(orderListTest);
        _mapper.Map<IEnumerable<OrderResult>>(orderListTest);

        // Action
        var result = await _orderAppService.GetOrdersByPortfolioIdAsync(portfolioTest.Id).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        _mockOrderService.Verify(orderService => orderService.GetOrdersByPortfolioIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_GetOrdersByProductIdAsync_Successfully()
    {
        // Arrange
        var orderListTest = OrderFixture.GenerateListOrderFixture(3);
        var productTest = ProductFixture.GenerateProductFixture();

        _mockOrderService.Setup(orderService => orderService.GetOrdersByProductIdAsync(It.IsAny<long>())).ReturnsAsync(orderListTest);
        _mapper.Map<IEnumerable<OrderResult>>(orderListTest);

        // Action
        var result = await _orderAppService.GetOrdersByProductIdAsync(productTest.Id).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        _mockOrderService.Verify(orderService => orderService.GetOrdersByProductIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public void Should_UpdateOrderAsync_Successfully()
    {
        // Arrange
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();
        var orderTest = OrderFixture.GenerateOrderFixture();

        _mockOrderService.Setup(orderService => orderService.Update(It.IsAny<Order>()));

        // Action
        _orderAppService.Update(updateOrderTest);

        // Assert
        _mockOrderService.Verify(orderService => orderService.Update(It.IsAny<Order>()), Times.Once);
    }
}