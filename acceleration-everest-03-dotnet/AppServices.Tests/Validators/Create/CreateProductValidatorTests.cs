using AppServices.Tests.Fixtures.Products;
using AppServices.Validators.Create;
using FluentAssertions;
using System;

namespace AppServices.Tests.Validators.Create;

public class CreateProductValidatorTests
{
    [Fact]
    public void Should_CreateProduct_Valid_Successfully()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        var validCreateProduct = new CreateProductValidator();

        // Action
        var result = validCreateProduct.Validate(createProductTest);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_NotCreateProduct_Invalid_Symbol_Empty_Successfully()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.Symbol = "";
        var validCreateProduct = new CreateProductValidator();

        // Action
        var result = validCreateProduct.Validate(createProductTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateProduct_Invalid_Symbol_LessThan3Characters_Successfully()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.Symbol = "ab";
        var validCreateProduct = new CreateProductValidator();

        // Action
        var result = validCreateProduct.Validate(createProductTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateProduct_Invalid_UnitPrice_EqualOrLessThan0_Successfully()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.UnitPrice = 0;
        var validCreateProduct = new CreateProductValidator();

        // Action
        var result = validCreateProduct.Validate(createProductTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateProduct_Invalid_IssuanceAt_BeforeToday_Successfully()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.IssuanceAt = DateTime.Now.AddDays(-1);
        var validCreateProduct = new CreateProductValidator();

        // Action
        var result = validCreateProduct.Validate(createProductTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateProduct_Invalid_ExpirationAt_BeforeToday_Successfully()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.ExpirationAt = DateTime.Now.AddDays(-1);
        var validCreateProduct = new CreateProductValidator();

        // Action
        var result = validCreateProduct.Validate(createProductTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateProduct_Invalid_Type_LessThan1_Successfully()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.Type = 0;
        var validCreateProduct = new CreateProductValidator();

        // Action
        var result = validCreateProduct.Validate(createProductTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateProduct_Invalid_Type_GreaterThan5_Successfully()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.Type = 6;
        var validCreateProduct = new CreateProductValidator();

        // Action
        var result = validCreateProduct.Validate(createProductTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
