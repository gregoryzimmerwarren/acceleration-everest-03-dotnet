using AppServices.Tests.Fixtures.Portfolios;
using AppServices.Validators.Create;
using FluentAssertions;

namespace AppServices.Tests.Validators.Create;

public class CreatePortfolioValidatorTests
{
    [Fact]
    public void Should_CreatePortfolio_Valid_Successfully()
    {
        // Arrange
        var createPortfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
        var validCreatePortfolio = new CreatePortfolioValidator();

        // Action
        var result = validCreatePortfolio.Validate(createPortfolioTest);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_NotCreatePortfolio_When_Name_Empty()
    {
        // Arrange
        var createPortfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
        createPortfolioTest.Name = "";
        var validCreatePortfolio = new CreatePortfolioValidator();

        // Action
        var result = validCreatePortfolio.Validate(createPortfolioTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreatePortfolio_When_Description_Empty()
    {
        // Arrange
        var createPortfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
        createPortfolioTest.Description = "";
        var validCreatePortfolio = new CreatePortfolioValidator();

        // Action
        var result = validCreatePortfolio.Validate(createPortfolioTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreatePortfolio_When_Description_LessThan5Characters()
    {
        // Arrange
        var createPortfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
        createPortfolioTest.Description = "Test";
        var validCreatePortfolio = new CreatePortfolioValidator();

        // Action
        var result = validCreatePortfolio.Validate(createPortfolioTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreatePortfolio_When_CustomerId_LessThan1()
    {
        // Arrange
        var createPortfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
        createPortfolioTest.CustomerId = 0;
        var validCreatePortfolio = new CreatePortfolioValidator();

        // Action
        var result = validCreatePortfolio.Validate(createPortfolioTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
