using AppServices.Tests.Fixtures.Orders;
using AppServices.Validators.Update;
using FluentAssertions;
using System;

namespace AppServices.Tests.Validators.Update;

public class UpdateOrderValidatorTests
{
    [Fact]
    public void Should_UpdateOrder_Valid_Successfully()
    {
        // Arrange
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();
        updateOrderTest.UnitPrice = 1;
        
        var validUpdateOrder = new UpdateOrderValidator();

        // Action
        var result = validUpdateOrder.Validate(updateOrderTest);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_NotUpdateOrder_When_Quotes_LessThan1()
    {
        // Arrange
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();
        updateOrderTest.UnitPrice = 1;
        
        updateOrderTest.Quotes = 0;
        var validUpdateOrder = new UpdateOrderValidator();

        // Action
        var result = validUpdateOrder.Validate(updateOrderTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateOrder_When_UnitPrice_EqualOrLessThan0()
    {
        // Arrange
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();
        updateOrderTest.UnitPrice = 0;
        
        var validUpdateOrder = new UpdateOrderValidator();

        // Action
        var result = validUpdateOrder.Validate(updateOrderTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateOrder_When_LiquidatedAt_BeforeToday()
    {
        // Arrange
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();
        updateOrderTest.UnitPrice = 1;
        
        updateOrderTest.LiquidatedAt = DateTime.Now.AddDays(-1);
        var validUpdateOrder = new UpdateOrderValidator();

        // Action
        var result = validUpdateOrder.Validate(updateOrderTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateOrder_When_PortfolioId_LessThan1()
    {
        // Arrange
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();
        updateOrderTest.UnitPrice = 1;
        updateOrderTest.PortfolioId = 0;
        var validUpdateOrder = new UpdateOrderValidator();

        // Action
        var result = validUpdateOrder.Validate(updateOrderTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateOrder_When_ProductId_LessThan1()
    {
        // Arrange
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();
        updateOrderTest.UnitPrice = 1;
        updateOrderTest.ProductId = 0;
        var validUpdateOrder = new UpdateOrderValidator();

        // Action
        var result = validUpdateOrder.Validate(updateOrderTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}