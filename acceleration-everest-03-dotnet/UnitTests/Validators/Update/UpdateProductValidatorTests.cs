using AppServices.Validators.Update;
using FluentAssertions;
using FluentValidation.TestHelper;
using UnitTests.Fixtures.Products;

namespace UnitTests.Validators.Update;

public class UpdateProductValidatorTests
{
    private readonly UpdateProductValidator _validUpdateProduct;

    public UpdateProductValidatorTests()
    {
        _validUpdateProduct = new UpdateProductValidator();
    }

    [Fact]
    public void Should_UpdateProduct_Valid_Successfully()
    {
        // Arrange
        var updateProductTest = UpdateProductFixture.GenerateUpdateProductFixture();

        // Action
        var result = _validUpdateProduct.Validate(updateProductTest);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldNot_UpdateProduct_When_Symbol_Empty()
    {
        // Arrange
        var updateProductTest = UpdateProductFixture.GenerateUpdateProductFixture();
        updateProductTest.Symbol = string.Empty;

        // Action
        var result = _validUpdateProduct.TestValidate(updateProductTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateProduct => updateProduct.Symbol);
    }

    [Fact]
    public void ShouldNot_UpdateProduct_When_Symbol_LessThan3Characters()
    {
        // Arrange
        var updateProductTest = UpdateProductFixture.GenerateUpdateProductFixture();
        updateProductTest.Symbol = "ab";

        // Action
        var result = _validUpdateProduct.TestValidate(updateProductTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateProduct => updateProduct.Symbol);
    }

    [Fact]
    public void ShouldNot_UpdateProduct_When_UnitPrice_EqualOrLessThan0()
    {
        // Arrange
        var updateProductTest = UpdateProductFixture.GenerateUpdateProductFixture();
        updateProductTest.UnitPrice = 0;

        // Action
        var result = _validUpdateProduct.TestValidate(updateProductTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateProduct => updateProduct.UnitPrice);
    }

    [Fact]
    public void ShouldNot_UpdateProduct_When_ExpirationAt_BeforeToday_Successfully()
    {
        // Arrange
        var updateProductTest = UpdateProductFixture.GenerateUpdateProductFixture();
        updateProductTest.ExpirationAt = DateTime.Now.AddDays(-1);

        // Action
        var result = _validUpdateProduct.TestValidate(updateProductTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateProduct => updateProduct.ExpirationAt);
    }
}