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
    public void Should_NotCreatePortfolio_Invalid_Name_Empty_Successfully()
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
    public void Should_NotCreatePortfolio_Invalid_Description_Empty_Successfully()
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
    public void Should_NotCreatePortfolio_Invalid_Description_LessThan5Characters_Successfully()
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
    public void Should_NotCreatePortfolio_Invalid_CustomerId_LessThan1_Successfully()
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
