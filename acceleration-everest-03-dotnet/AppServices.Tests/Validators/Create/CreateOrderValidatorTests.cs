using AppModels.Enums;
using AppServices.Tests.Fixtures.Orders;
using AppServices.Validators.Create;
using FluentAssertions;
using System;

namespace AppServices.Tests.Validators.Create;

public class CreateOrderValidatorTests
{
    [Fact]
    public void Should_CreateOrder_Valid_Successfully()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.UnitPrice = 1;
        createOrderTest.Direction = OrderDirection.Buy;
        var validCreateOrder = new CreateOrderValidator();

        // Action
        var result = validCreateOrder.Validate(createOrderTest);

        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void Should_NotCreateOrder_Invalid_Quotes_LessThan1_Successfully()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.UnitPrice = 1;
        createOrderTest.Direction = OrderDirection.Buy;
        createOrderTest.Quotes = 0;
        var validCreateOrder = new CreateOrderValidator();

        // Action
        var result = validCreateOrder.Validate(createOrderTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void Should_NotCreateOrder_Invalid_UnitPrice_EqualOrLessThan0_Successfully()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.UnitPrice = 0;
        createOrderTest.Direction = OrderDirection.Buy;
        var validCreateOrder = new CreateOrderValidator();

        // Action
        var result = validCreateOrder.Validate(createOrderTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateOrder_Invalid_LiquidatedAt_BeforeToday_Successfully()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.UnitPrice = 1;
        createOrderTest.Direction = OrderDirection.Buy;
        createOrderTest.LiquidatedAt = DateTime.Now.AddDays(-1);
        var validCreateOrder = new CreateOrderValidator();

        // Action
        var result = validCreateOrder.Validate(createOrderTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateOrder_Invalid_Direction_Successfully()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.UnitPrice = 1;
        createOrderTest.Direction = 0;
        var validCreateOrder = new CreateOrderValidator();

        // Action
        var result = validCreateOrder.Validate(createOrderTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateOrder_Invalid_PortfolioId_LessThan1_Successfully()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.UnitPrice = 1;
        createOrderTest.Direction = OrderDirection.Sell;
        createOrderTest.PortfolioId = 0;
        var validCreateOrder = new CreateOrderValidator();

        // Action
        var result = validCreateOrder.Validate(createOrderTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateOrder_Invalid_ProductId_LessThan1_Successfully()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.UnitPrice = 1;
        createOrderTest.Direction = OrderDirection.Sell;
        createOrderTest.ProductId = 0;
        var validCreateOrder = new CreateOrderValidator();

        // Action
        var result = validCreateOrder.Validate(createOrderTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}