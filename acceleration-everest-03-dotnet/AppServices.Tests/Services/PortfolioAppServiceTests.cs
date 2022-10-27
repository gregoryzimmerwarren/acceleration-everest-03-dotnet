using AppModels.Portfolios;
using AppServices.Interfaces;
using AppServices.Services;
using AppServices.Tests.Fixtures.Customers;
using AppServices.Tests.Fixtures.Portfolios;
using AppServices.Tests.Fixtures.Products;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using FluentAssertions;
using Moq;
using System;

namespace AppServices.Tests.Services;

public class PortfolioAppServiceTests
{
    private readonly Mock<ICustomerBankInfoAppService> _mockCustomerBankInfoAppService;
    private readonly Mock<IPortfolioProductAppService> _mockPortfolioProductAppService;
    private readonly Mock<IProductAppService> _mockProductAppService;
    private readonly Mock<IPortfolioService> _mockPortfolioService;
    private readonly Mock<IOrderAppService> _mockOrderAppService;
    private readonly PortfolioAppService _portfolioAppService;
    private readonly IMapper _mapper;

    public PortfolioAppServiceTests( )
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Portfolio, PortfolioResult>();
            cfg.CreateMap<Portfolio, PortfolioResultForOthersDtos>();
            cfg.CreateMap<CreatePortfolio, Portfolio>();
        });
        _mapper = config.CreateMapper();
        _mockOrderAppService = new Mock<IOrderAppService>();
        _mockPortfolioService = new Mock<IPortfolioService>();
        _mockProductAppService = new Mock<IProductAppService>();
        _mockPortfolioProductAppService = new Mock<IPortfolioProductAppService>();
        _mockCustomerBankInfoAppService = new Mock<ICustomerBankInfoAppService>();
        _portfolioAppService = new PortfolioAppService(_mockPortfolioProductAppService.Object, _mockCustomerBankInfoAppService.Object,
            _mockProductAppService.Object, _mockPortfolioService.Object, _mockOrderAppService.Object, _mapper);
    }

    [Fact]
    public void Should_CreatePortfolio_Successfully()
    {
        // Arrange  
        var createPortfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        long idTest = 1;

        _mockPortfolioService.Setup(portfolioService => portfolioService.Create(It.IsAny<Portfolio>())).Returns(idTest);

        // Action
        var result = _portfolioAppService.Create(createPortfolioTest);

        // Assert
        result.Should().Be(idTest);

        _mockPortfolioService.Verify(portfolioService => portfolioService.Create(It.IsAny<Portfolio>()), Times.Once);
    }

    [Fact]
    public async void Should_DeleteCustomer_Successfully()
    {
        // Arrange  
        long idTest = 1;

        _mockPortfolioService.Setup(portfolioService => portfolioService.DeleteAsync(It.IsAny<long>()));

        // Action
        await _portfolioAppService.DeleteAsync(idTest).ConfigureAwait(false);

        // Assert
        _mockPortfolioService.Verify(portfolioService => portfolioService.DeleteAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_DepositPortfolioAsync_Successfully()
    {
        // Arrage
        long idTest = 1;
        var customerTest = CustomerFixture.GenerateCustomerFixture();
        customerTest.Id = idTest;
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        portfolioTest.Id = idTest;
        decimal amountTest = 17.05m;
        decimal totalBankInfo = 20m;

        _mockCustomerBankInfoAppService.Setup(customerBankInfoService => customerBankInfoService.GetTotalByCustomerIdAsync(It.IsAny<long>())).ReturnsAsync(totalBankInfo);
        _mockCustomerBankInfoAppService.Setup(customerBankInfoService => customerBankInfoService.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>())).ReturnsAsync(It.IsAny<bool>());
        _mockPortfolioService.Setup(portfolioService => portfolioService.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()));

        // Action
        await _portfolioAppService.DepositAsync(customerTest.Id, portfolioTest.Id, amountTest).ConfigureAwait(false);

        // Assert
        _mockCustomerBankInfoAppService.Verify(customerBankInfoService => customerBankInfoService.GetTotalByCustomerIdAsync(It.IsAny<long>()), Times.Once);
        _mockCustomerBankInfoAppService.Verify(customerBankInfoService => customerBankInfoService.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        _mockPortfolioService.Verify(portfolioService => portfolioService.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
    }

    [Fact]
    public async void Should_Not_DepositPortfolioAsync_Throwing_ArgumentException()
    {
        // Arrage
        long idTest = 1;
        var customerTest = CustomerFixture.GenerateCustomerFixture();
        customerTest.Id = idTest;
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        portfolioTest.Id = idTest;
        decimal amountTest = 20m;
        decimal totalBankInfo = 17.05m;

        _mockCustomerBankInfoAppService.Setup(customerBankInfoService => customerBankInfoService.GetTotalByCustomerIdAsync(It.IsAny<long>())).ReturnsAsync(totalBankInfo);

        // Action
        var action = () => _portfolioAppService.DepositAsync(customerTest.Id, portfolioTest.Id, amountTest);

        // Assert
        await action.Should().ThrowAsync<ArgumentException>();
        _mockCustomerBankInfoAppService.Verify(customerBankInfoService => customerBankInfoService.GetTotalByCustomerIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_ExecuteBuyOrderAsync_WithNoRelation_Between_PortfolioAndProduct_Successfully()
    {
        long idTest = 1;
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        portfolioTest.Id = idTest;
        var productTest = ProductFixture.GenerateProductFixture();
        productTest.Id = idTest;
        decimal amount = 17.05m;

    }
}