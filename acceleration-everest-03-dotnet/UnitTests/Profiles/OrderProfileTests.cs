using AppModels.Orders;
using AppServices.Profiles;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using UnitTests.Fixtures.Orders;
using UnitTests.Fixtures.Portfolios;
using UnitTests.Fixtures.Products;

namespace UnitTests.Profiles;

public class OrderProfileTests : OrderProfile
{
    private readonly IMapper _mapper;

    public OrderProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Order, OrderResult>();
            cfg.CreateMap<Order, OrderResultForOtherDtos>();
            cfg.CreateMap<CreateOrder, Order>();
            cfg.CreateMap<UpdateOrder, Order>();
        });
        _mapper = config.CreateMapper();
    }


    [Fact]
    public void Should_MapTo_OrderResult_FromOrder_Successfully()
    {
        // Arrange
        var portfolioResultForOthersDtosTest = PortfolioResultForOthersDtosFixture.GeneratePortfolioResultForOthersDtosFixture();
        var productResultTest = ProductResultFixture.GenerateProductResultFixture();
        var orderTest = OrderFixture.GenerateOrderFixture();
        var orderResultTest = new OrderResult(
            id: orderTest.Id,
            quotes: orderTest.Quotes,
            unitPrice: orderTest.UnitPrice,
            netValue: orderTest.NetValue,
            liquidatedAt: orderTest.LiquidatedAt,
            direction: orderTest.Direction,
            wasExecuted: orderTest.WasExecuted,
            portfolio: portfolioResultForOthersDtosTest,
            product: productResultTest);

        // Action
        var result = _mapper.Map<OrderResult>(orderTest);
        result.Portfolio = portfolioResultForOthersDtosTest;
        result.Product = productResultTest;

        // Assert
        result.Should().BeEquivalentTo(orderResultTest);
    }

    [Fact]
    public void Should_MapTo_OrderResultForOtherDtos_FromOrder_Successfully()
    {
        // Arrange
        var productResultTest = ProductResultFixture.GenerateProductResultFixture();
        var orderTest = OrderFixture.GenerateOrderFixture();
        var orderResultTest = new OrderResultForOtherDtos(
            id: orderTest.Id,
            quotes: orderTest.Quotes,
            unitPrice: orderTest.UnitPrice,
            netValue: orderTest.NetValue,
            liquidatedAt: orderTest.LiquidatedAt,
            direction: orderTest.Direction,
            wasExecuted: orderTest.WasExecuted,
            product: productResultTest);

        // Action
        var result = _mapper.Map<OrderResultForOtherDtos>(orderTest);
        result.Product = productResultTest;

        // Assert
        result.Should().BeEquivalentTo(orderResultTest);
    }

    [Fact]
    public void Should_MapTo_Orders_FromCreateOrder_Successfully()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        var orderTest = new Order(
            quotes: createOrderTest.Quotes,
            unitPrice: createOrderTest.UnitPrice,
            liquidatedAt: createOrderTest.LiquidatedAt,
            direction: createOrderTest.Direction,
            wasExecuted: createOrderTest.WasExecuted,
            portfolioId: 1,
            productId: 1);

        // Action
        var result = _mapper.Map<Order>(createOrderTest);

        // Assert
        result.Should().BeEquivalentTo(orderTest);
    }

    [Fact]
    public void Should_MapTo_Orders_FromUpdateOrder_Successfully()
    {
        // Arrange
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();
        var orderTest = new Order(
            quotes: updateOrderTest.Quotes,
            unitPrice: updateOrderTest.UnitPrice,
            liquidatedAt: updateOrderTest.LiquidatedAt,
            direction: updateOrderTest.Direction,
            wasExecuted: updateOrderTest.WasExecuted,
            portfolioId: 1,
            productId: 1);

        // Action
        var result = _mapper.Map<Order>(updateOrderTest);
        result.NetValue = result.UnitPrice * result.Quotes;
        result.Id = 0;

        // Assert
        result.Should().BeEquivalentTo(orderTest);
    }
}