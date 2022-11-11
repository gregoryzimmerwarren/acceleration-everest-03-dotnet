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

    [Theory]
    [InlineData("")]
    [InlineData("ab")]
    public void ShouldNot_CreateProduct_When_Symbol_Empty_Or_LessThan3Characters(string symbol)
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.Symbol = symbol;

        // Action
        var result = _validCreateProduct.TestValidate(createProductTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createProduct => createProduct.Symbol);
    }

    [Fact]
    public void ShouldNot_CreateProduct_When_UnitPrice_EqualOrLessThan0()
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
    public void ShouldNot_CreateProduct_When_ExpirationAt_BeforeToday()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.ExpirationAt = DateTime.Now.AddDays(-1);

        // Action
        var result = _validCreateProduct.TestValidate(createProductTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createProduct => createProduct.ExpirationAt);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    public void ShouldNot_CreateProduct_When_Type_LessThan1_Or_GreaterThan5(int type)
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        createProductTest.Type = type;

        // Action
        var result = _validCreateProduct.TestValidate(createProductTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createProduct => createProduct.Type);
    }
}