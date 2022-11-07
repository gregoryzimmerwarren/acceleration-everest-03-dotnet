﻿using AppModels.Orders;
using AppModels.Products;
using AppServices.Interfaces;
using AppServices.Services;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using FluentAssertions;
using Moq;
using UnitTests.Fixtures.Orders;
using UnitTests.Fixtures.Portfolios;
using UnitTests.Fixtures.Products;

namespace UnitTests.AppServices;

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
            cfg.CreateMap<Order, OrderResultForOtherDtos>();
            cfg.CreateMap<CreateOrder, Order>();
            cfg.CreateMap<UpdateOrder, Order>();
        });
        _mapper = config.CreateMapper();
        _mockOrderService = new Mock<IOrderService>();
        _mockProductAppService = new Mock<IProductAppService>();
        _orderAppService = new OrderAppService(_mockProductAppService.Object, _mockOrderService.Object, _mapper);
    }

    [Fact]
    public void Should_CreateOrderAsync_Successfully()
    {
        // Arrange
        var createdOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        var orderTest = OrderFixture.GenerateOrderFixture();

        _mockOrderService.Setup(orderService => orderService.Create(It.IsAny<Order>())).Returns(orderTest.Id);

        // Action
        var resultOrder = _orderAppService.Create(createdOrderTest);

        // Assert
        resultOrder.Should().NotBe(0);
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
    public async void Should_GetOrderByIdAsync_Successfully()
    {
        // Arrange
        var orderTest = OrderFixture.GenerateOrderFixture();

        _mockOrderService.Setup(orderService => orderService.GetOrderByIdAsync(It.IsAny<long>())).ReturnsAsync(orderTest);
        _mapper.Map<OrderResult>(orderTest);

        // Action
        var result = await _orderAppService.GetOrderByIdAsync(It.IsAny<long>()).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        _mockOrderService.Verify(orderService => orderService.GetOrderByIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_GetAvailableQuotes_Successfully()
    {
        // Arrange
        var listOrderTest = OrderFixture.GenerateListOrderFixture(3);

        _mockOrderService.Setup(orderService => orderService.GetAvailableQuotes(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(It.IsAny<int>());

        // Action
        var result = await _orderAppService.GetAvailableQuotes(It.IsAny<long>(), It.IsAny<long>()).ConfigureAwait(false);

        // Assert
        result.Should().NotBe(null);
        _mockOrderService.Verify(orderService => orderService.GetAvailableQuotes(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
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
        var result = await _orderAppService.GetOrdersByPortfolioIdAsync(It.IsAny<long>()).ConfigureAwait(false);

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
        var result = await _orderAppService.GetOrdersByProductIdAsync(It.IsAny<long>()).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        _mockOrderService.Verify(orderService => orderService.GetOrdersByProductIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public void Should_UpdateOrder_Successfully()
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