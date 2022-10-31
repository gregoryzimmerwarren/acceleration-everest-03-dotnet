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
    public void Should_NotUpdateOrder_Invalid_Quotes_LessThan1_Successfully()
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
    public void Should_NotUpdateOrder_Invalid_UnitPrice_EqualOrLessThan0_Successfully()
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
    public void Should_NotUpdateOrder_Invalid_LiquidatedAt_BeforeToday_Successfully()
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
    public void Should_NotUpdateOrder_Invalid_PortfolioId_LessThan1_Successfully()
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
    public void Should_NotUpdateOrder_Invalid_ProductId_LessThan1_Successfully()
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