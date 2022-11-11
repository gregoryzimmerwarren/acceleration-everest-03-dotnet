using AppServices.Validators.Update;
using FluentAssertions;
using FluentValidation.TestHelper;
using UnitTests.Fixtures.Orders;

namespace UnitTests.Validators.Update;

public class UpdateOrderValidatorTests
{
    private readonly UpdateOrderValidator _validUpdateOrder;

    public UpdateOrderValidatorTests()
    {
        _validUpdateOrder = new UpdateOrderValidator();
    }

    [Fact]
    public void Should_UpdateOrder_Valid_Successfully()
    {
        // Arrange
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();
        updateOrderTest.UnitPrice = 1;

        // Action
        var result = _validUpdateOrder.Validate(updateOrderTest);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldNot_UpdateOrder_When_Quotes_LessThan1()
    {
        // Arrange
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();
        updateOrderTest.UnitPrice = 1;
        updateOrderTest.Quotes = 0;

        // Action
        var result = _validUpdateOrder.TestValidate(updateOrderTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateOrder => updateOrder.Quotes);
    }

    [Fact]
    public void ShouldNot_UpdateOrder_When_UnitPrice_EqualOrLessThan0()
    {
        // Arrange
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();
        updateOrderTest.UnitPrice = 0;

        // Action
        var result = _validUpdateOrder.TestValidate(updateOrderTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateOrder => updateOrder.UnitPrice);
    }

    [Fact]
    public void ShouldNot_UpdateOrder_When_LiquidatedAt_BeforeToday()
    {
        // Arrange
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();
        updateOrderTest.UnitPrice = 1;
        updateOrderTest.LiquidatedAt = DateTime.Now.AddDays(-1);

        // Action
        var result = _validUpdateOrder.TestValidate(updateOrderTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateOrder => updateOrder.LiquidatedAt);
    }

    [Fact]
    public void ShouldNot_UpdateOrder_When_PortfolioId_LessThan1()
    {
        // Arrange
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();
        updateOrderTest.UnitPrice = 1;
        updateOrderTest.PortfolioId = 0;

        // Action
        var result = _validUpdateOrder.TestValidate(updateOrderTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateOrder => updateOrder.PortfolioId);
    }

    [Fact]
    public void ShouldNot_UpdateOrder_When_ProductId_LessThan1()
    {
        // Arrange
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();
        updateOrderTest.UnitPrice = 1;
        updateOrderTest.ProductId = 0;

        // Action
        var result = _validUpdateOrder.TestValidate(updateOrderTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateOrder => updateOrder.ProductId);
    }
}