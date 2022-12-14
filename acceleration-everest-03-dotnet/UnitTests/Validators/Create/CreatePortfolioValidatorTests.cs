using AppServices.Validators.Create;
using FluentAssertions;
using FluentValidation.TestHelper;
using UnitTests.Fixtures.Portfolios;

namespace UnitTests.Validators.Create;

public class CreatePortfolioValidatorTests
{
    private readonly CreatePortfolioValidator _validCreatePortfolio;

    public CreatePortfolioValidatorTests()
    {
        _validCreatePortfolio = new CreatePortfolioValidator();
    }

    [Fact]
    public void Should_CreatePortfolio_Valid_Successfully()
    {
        // Arrange
        var createPortfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();

        // Action
        var result = _validCreatePortfolio.Validate(createPortfolioTest);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldNot_CreatePortfolio_When_Name_Empty()
    {
        // Arrange
        var createPortfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
        createPortfolioTest.Name = string.Empty;

        // Action
        var result = _validCreatePortfolio.TestValidate(createPortfolioTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createPortfolio => createPortfolio.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Test")]
    public void ShouldNot_CreatePortfolio_When_Description_Empty_Or_LessThan5Characters(string description)
    {
        // Arrange
        var createPortfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
        createPortfolioTest.Description = description;

        // Action
        var result = _validCreatePortfolio.TestValidate(createPortfolioTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createPortfolio => createPortfolio.Description);
    }

    [Fact]
    public void ShouldNot_CreatePortfolio_When_CustomerId_LessThan1()
    {
        // Arrange
        var createPortfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
        createPortfolioTest.CustomerId = 0;

        // Action
        var result = _validCreatePortfolio.TestValidate(createPortfolioTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createPortfolio => createPortfolio.CustomerId);
    }
}