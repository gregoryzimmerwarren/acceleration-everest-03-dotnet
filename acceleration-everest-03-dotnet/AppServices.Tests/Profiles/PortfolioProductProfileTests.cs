using AppModels.PortfoliosProducts;
using AppServices.Tests.Fixtures.Portfolios;
using AppServices.Tests.Fixtures.PortfoliosProducts;
using AppServices.Tests.Fixtures.Products;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;

namespace AppServices.Tests.Profiles;

public class PortfolioProductProfileTests
{
    private readonly IMapper _mapper;

    public PortfolioProductProfileTests()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.CreateMap<PortfolioProduct, PortfolioProductResult>();
        });
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Should_MapTo_PortfolioProductResult_FromPortfolioProduct_Successfully()
    {
        // Arrange
        var portfolioResultForOthersDtosTest = PortfolioResultForOthersDtosFixture.GeneratePortfolioResultForOthersDtosFixture();
        var productResultTest = ProductResultFixture.GenerateProductResultFixture();
        var portfolioProductTest = PortfolioProductFixture.GeneratePortfolioProductFixture();
        var portfolioProductResultTest = new PortfolioProductResult(
            id: portfolioProductTest.Id,
            portfolio: portfolioResultForOthersDtosTest,
            product: productResultTest);

        // Action
        var result = _mapper.Map<PortfolioProductResult>(portfolioProductTest);
        result.Portfolio = portfolioResultForOthersDtosTest;
        result.Product = productResultTest;

        // Assert
        result.Should().BeEquivalentTo(portfolioProductResultTest);
    }
}