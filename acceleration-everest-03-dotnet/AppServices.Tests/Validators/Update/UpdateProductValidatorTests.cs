using AppServices.Tests.Fixtures.Products;
using AppServices.Validators.Update;
using FluentAssertions;
using System;

namespace AppServices.Tests.Validators.Update;

public class UpdateProductValidatorTests
{
    [Fact]
    public void Should_UpdateProduct_Valid_Successfully()
    {
        // Arrange
        var updateProductTest = UpdateProductFixture.GenerateUpdateProductFixture();
        var validUpdateProduct = new UpdateProductValidator();

        // Action
        var result = validUpdateProduct.Validate(updateProductTest);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_NotUpdateProduct_When_Symbol_Empty()
    {
        // Arrange
        var updateProductTest = UpdateProductFixture.GenerateUpdateProductFixture();
        updateProductTest.Symbol = "";
        var validUpdateProduct = new UpdateProductValidator();

        // Action
        var result = validUpdateProduct.Validate(updateProductTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateProduct_When_Symbol_LessThan3Characters()
    {
        // Arrange
        var updateProductTest = UpdateProductFixture.GenerateUpdateProductFixture();
        updateProductTest.Symbol = "ab";
        var validUpdateProduct = new UpdateProductValidator();

        // Action
        var result = validUpdateProduct.Validate(updateProductTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateProduct_When_UnitPrice_EqualOrLessThan0()
    {
        // Arrange
        var updateProductTest = UpdateProductFixture.GenerateUpdateProductFixture();
        updateProductTest.UnitPrice = 0;
        var validUpdateProduct = new UpdateProductValidator();

        // Action
        var result = validUpdateProduct.Validate(updateProductTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateProduct_When_IssuanceAt_BeforeToday()
    {
        // Arrange
        var updateProductTest = UpdateProductFixture.GenerateUpdateProductFixture();
        updateProductTest.IssuanceAt = DateTime.Now.AddDays(-1);
        var validUpdateProduct = new UpdateProductValidator();

        // Action
        var result = validUpdateProduct.Validate(updateProductTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateProduct_When_ExpirationAt_BeforeToday_Successfully()
    {
        // Arrange
        var updateProductTest = UpdateProductFixture.GenerateUpdateProductFixture();
        updateProductTest.ExpirationAt = DateTime.Now.AddDays(-1);
        var validUpdateProduct = new UpdateProductValidator();

        // Action
        var result = validUpdateProduct.Validate(updateProductTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
