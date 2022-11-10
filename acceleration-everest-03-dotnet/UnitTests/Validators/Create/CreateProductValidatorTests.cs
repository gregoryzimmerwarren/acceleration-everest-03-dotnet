using AppServices.Validators.Create;
using FluentAssertions;
using FluentValidation.TestHelper;
using UnitTests.Fixtures.Products;

namespace UnitTests.Validators.Create;

public class CreateProductValidatorTests
{
    private readonly CreateProductValidator _validCreateProduct;

    public CreateProductValidatorTests()
    {
        _validCreateProduct = new CreateProductValidator();
    }

    [Fact]
    public void Should_CreateProduct_Valid_Successfully()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();

        // Action
        var result = _validCreateProduct.Validate(createProductTest);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_NotCreateProduct_When_Symbol_Empty()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.Symbol = string.Empty;

        // Action
        var result = _validCreateProduct.TestValidate(createProductTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createProduct => createProduct.Symbol);
    }

    [Fact]
    public void Should_NotCreateProduct_When_Symbol_LessThan3Characters()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.Symbol = "ab";

        // Action
        var result = _validCreateProduct.TestValidate(createProductTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createProduct => createProduct.Symbol);
    }

    [Fact]
    public void Should_NotCreateProduct_When_UnitPrice_EqualOrLessThan0()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.UnitPrice = 0;

        // Action
        var result = _validCreateProduct.TestValidate(createProductTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createProduct => createProduct.UnitPrice);
    }

    [Fact]
    public void Should_NotCreateProduct_When_ExpirationAt_BeforeToday()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.ExpirationAt = DateTime.Now.AddDays(-1);

        // Action
        var result = _validCreateProduct.TestValidate(createProductTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createProduct => createProduct.ExpirationAt);
    }

    [Fact]
    public void Should_NotCreateProduct_When_Type_LessThan1()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.Type = 0;

        // Action
        var result = _validCreateProduct.TestValidate(createProductTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createProduct => createProduct.Type);
    }

    [Fact]
    public void Should_NotCreateProduct_When_Type_GreaterThan5()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.Type = 6;

        // Action
        var result = _validCreateProduct.TestValidate(createProductTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createProduct => createProduct.Type);
    }
}