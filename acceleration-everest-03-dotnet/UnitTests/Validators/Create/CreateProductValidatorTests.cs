using AppServices.Validators.Create;
using FluentAssertions;
using System;
using UnitTests.Fixtures.Products;

namespace UnitTests.Validators.Create;

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
    public void Should_NotCreateProduct_When_Symbol_Empty()
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
    public void Should_NotCreateProduct_When_Symbol_LessThan3Characters()
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
    public void Should_NotCreateProduct_When_UnitPrice_EqualOrLessThan0()
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
    public void Should_NotCreateProduct_When_ExpirationAt_BeforeToday()
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
    public void Should_NotCreateProduct_When_Type_LessThan1()
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
    public void Should_NotCreateProduct_When_Type_GreaterThan5()
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