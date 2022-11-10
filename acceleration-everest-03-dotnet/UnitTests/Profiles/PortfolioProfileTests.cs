using AppModels.Portfolios;
using AppServices.Profiles;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using UnitTests.Fixtures.Customers;
using UnitTests.Fixtures.Orders;
using UnitTests.Fixtures.Portfolios;

namespace UnitTests.Profiles;

public class PortfolioProfileTests : PortfolioProfile
{
    private readonly IMapper _mapper;

    public PortfolioProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Portfolio, PortfolioResult>();
            cfg.CreateMap<Portfolio, PortfolioResultForOthersDtos>();
            cfg.CreateMap<CreatePortfolio, Portfolio>();
        });
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Should_MapTo_PortfolioResult_FromPortfolio_Successfully()
    {
        // Arrange
        var customerResultForOtherDtosTest = CustomerResultForOtherDtosFixture.GenerateCustomerResultForOtherDtosFixture();

        var listOrderResultForOtherDtosTest = OrderResultOtherDtosFixture.GenerateListOrderResultOtherDtosFixture(2);

        var porfolioTest = PortfolioFixture.GeneratePortfolioFixture();

        var portfolioResultTest = new PortfolioResult(
            id: porfolioTest.Id,
            name: porfolioTest.Name,
            description: porfolioTest.Description,
            totalBalance: porfolioTest.TotalBalance,
            accountBalance: porfolioTest.AccountBalance,
            customer: customerResultForOtherDtosTest,
            orders: listOrderResultForOtherDtosTest);

        // Action
        var result = _mapper.Map<PortfolioResult>(porfolioTest);

        result.Customer = customerResultForOtherDtosTest;

        result.Orders = listOrderResultForOtherDtosTest;

        // Assert
        result.Should().BeEquivalentTo(portfolioResultTest);
    }

    [Fact]
    public void Should_MapTo_PortfolioResultForOthersDtos_FromPortfolio_Successfully()
    {
        // Arrange
        var porfolioTest = PortfolioFixture.GeneratePortfolioFixture();

        var portfolioResultForOtherDtosTest = new PortfolioResultForOthersDtos(
            id: porfolioTest.Id,
            name: porfolioTest.Name,
            totalBalance: porfolioTest.TotalBalance);

        // Action
        var result = _mapper.Map<PortfolioResultForOthersDtos>(porfolioTest);

        // Assert
        result.Should().BeEquivalentTo(portfolioResultForOtherDtosTest);
    }

    [Fact]
    public void Should_MapTo_Portfolio_FromCreatePortfolio_Successfully()
    {
        // Arrange
        var createPorfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();

        var portfolioTest = new Portfolio(
            name: createPorfolioTest.Name,
            description: createPorfolioTest.Description,
            totalBalance: 0,
            accountBalance: 0,
            customerId: createPorfolioTest.CustomerId);

        // Action
        var result = _mapper.Map<Portfolio>(createPorfolioTest);

        // Assert
        result.Should().BeEquivalentTo(portfolioTest);
    }
}