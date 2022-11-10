using AppServices.Validators.Create;
using FluentAssertions;
using FluentValidation.TestHelper;
using Infrastructure.CrossCutting.Enums;
using UnitTests.Fixtures.Orders;

namespace UnitTests.Validators.Create;

public class CreateOrderValidatorTests
{
    private readonly CreateOrderValidator _validCreateOrder;

    public CreateOrderValidatorTests()
    {
        _validCreateOrder = new CreateOrderValidator();
    }

    [Fact]
    public void Should_CreateOrder_Valid_Successfully()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.UnitPrice = 1;
        createOrderTest.Direction = OrderDirection.Buy;

        // Action
        var result = _validCreateOrder.Validate(createOrderTest);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_NotCreateOrder_When_Quotes_LessThan1()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.UnitPrice = 1;
        createOrderTest.Direction = OrderDirection.Buy;
        createOrderTest.Quotes = 0;

        // Action
        var result = _validCreateOrder.TestValidate(createOrderTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createOrder => createOrder.Quotes);
    }

    [Fact]
    public void Should_NotCreateOrder_When_UnitPrice_EqualOrLessThan0()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.UnitPrice = 0;
        createOrderTest.Direction = OrderDirection.Buy;

        // Action
        var result = _validCreateOrder.TestValidate(createOrderTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createOrder => createOrder.UnitPrice);
    }

    [Fact]
    public void Should_NotCreateOrder_When_LiquidatedAt_BeforeToday()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.UnitPrice = 1;
        createOrderTest.Direction = OrderDirection.Buy;
        createOrderTest.LiquidatedAt = DateTime.Now.AddDays(-1);

        // Action
        var result = _validCreateOrder.TestValidate(createOrderTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createOrder => createOrder.LiquidatedAt);
    }

    [Fact]
    public void Should_NotCreateOrder_When_Direction()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.UnitPrice = 1;
        createOrderTest.Direction = 0;

        // Action
        var result = _validCreateOrder.TestValidate(createOrderTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createOrder => createOrder.Direction);
    }

    [Fact]
    public void Should_NotCreateOrder_When_PortfolioId_LessThan1()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.UnitPrice = 1;
        createOrderTest.Direction = OrderDirection.Sell;
        createOrderTest.PortfolioId = 0;

        // Action
        var result = _validCreateOrder.TestValidate(createOrderTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createOrder => createOrder.PortfolioId);
    }

    [Fact]
    public void Should_NotCreateOrder_When_ProductId_LessThan1()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.UnitPrice = 1;
        createOrderTest.Direction = OrderDirection.Sell;
        createOrderTest.ProductId = 0;

        // Action
        var result = _validCreateOrder.TestValidate(createOrderTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createOrder => createOrder.ProductId);
    }
}