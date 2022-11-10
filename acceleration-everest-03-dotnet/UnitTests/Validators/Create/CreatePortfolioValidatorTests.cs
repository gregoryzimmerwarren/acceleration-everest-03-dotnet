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
    public void Should_NotCreatePortfolio_When_Name_Empty()
    {
        // Arrange
        var createPortfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
        createPortfolioTest.Name = string.Empty;

        // Action
        var result = _validCreatePortfolio.TestValidate(createPortfolioTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createPortfolio => createPortfolio.Name);
    }

    [Fact]
    public void Should_NotCreatePortfolio_When_Description_Empty()
    {
        // Arrange
        var createPortfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
        createPortfolioTest.Description = string.Empty;

        // Action
        var result = _validCreatePortfolio.TestValidate(createPortfolioTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createPortfolio => createPortfolio.Description);
    }

    [Fact]
    public void Should_NotCreatePortfolio_When_Description_LessThan5Characters()
    {
        // Arrange
        var createPortfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
        createPortfolioTest.Description = "Test";

        // Action
        var result = _validCreatePortfolio.TestValidate(createPortfolioTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createPortfolio => createPortfolio.Description);
    }

    [Fact]
    public void Should_NotCreatePortfolio_When_CustomerId_LessThan1()
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