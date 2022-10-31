using AppModels.Customers;
using AppModels.Orders;
using AppModels.Portfolios;
using AppModels.PortfoliosProducts;
using AppServices.Interfaces;
using AppServices.Services;
using AppServices.Tests.Fixtures.Customers;
using AppServices.Tests.Fixtures.Orders;
using AppServices.Tests.Fixtures.Portfolios;
using AppServices.Tests.Fixtures.PortfoliosProducts;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using DomainServices.Services;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace AppServices.Tests.Services;

public class PortfolioAppServiceTests
{
    private readonly Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>> _mockrepositoryFactory;
    private readonly Mock<ICustomerBankInfoAppService> _mockCustomerBankInfoAppService;
    private readonly Mock<IUnitOfWork<WarrenEverestDotnetDbContext>> _mockUnitOfWork;
    private readonly Mock<IPortfolioProductService> _mockPortfolioProductService;
    private readonly PortfolioProductService _portfolioProductService;
    private readonly Mock<IProductAppService> _mockProductAppService;
    private readonly Mock<IPortfolioService> _mockPortfolioService;
    private readonly Mock<IOrderAppService> _mockOrderAppService;
    private readonly PortfolioAppService _portfolioAppService;
    private readonly Mock<IOrderService> _mockOrderService;
    private readonly OrderAppService _orderAppService;
    private readonly IMapper _mapper;

    public PortfolioAppServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Portfolio, PortfolioResult>();
            cfg.CreateMap<Portfolio, PortfolioResultForOthersDtos>();
            cfg.CreateMap<CreatePortfolio, Portfolio>();
        });
        _mapper = config.CreateMapper();
        _mockOrderService = new Mock<IOrderService>();
        _mockOrderAppService = new Mock<IOrderAppService>();
        _mockPortfolioService = new Mock<IPortfolioService>();
        _mockProductAppService = new Mock<IProductAppService>();
        _mockPortfolioProductService = new Mock<IPortfolioProductService>();
        _mockUnitOfWork = new Mock<IUnitOfWork<WarrenEverestDotnetDbContext>>();
        _mockCustomerBankInfoAppService = new Mock<ICustomerBankInfoAppService>();
        _mockrepositoryFactory = new Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>>();
        _orderAppService = new OrderAppService(_mockProductAppService.Object, _mockOrderService.Object, _mapper);
        _portfolioProductService = new PortfolioProductService(_mockUnitOfWork.Object, _mockrepositoryFactory.Object);
        _portfolioAppService = new PortfolioAppService(_mockCustomerBankInfoAppService.Object, _mockPortfolioProductService.Object,
            _mockProductAppService.Object, _mockPortfolioService.Object, _mockOrderAppService.Object, _mapper);
    }

    [Fact]
    public void Should_CreatePortfolio_Successfully()
    {
        // Arrange  
        var createPortfolioTest = CreatePortfolioFixture.GenerateCreatePortfolioFixture();
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();

        _mockPortfolioService.Setup(portfolioService => portfolioService.Create(It.IsAny<Portfolio>())).Returns(It.IsAny<long>());

        // Action
        var result = _portfolioAppService.Create(createPortfolioTest);

        // Assert
        result.Should().NotBe(null);

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
        long customerIdTest = 1;
        long portfolioIdTest = 1;
        decimal amountTest = 17.05m;
        decimal totalBankInfo = 20m;

        _mockCustomerBankInfoAppService.Setup(customerBankInfoAppService => customerBankInfoAppService.GetAccountBalanceByCustomerIdAsync(It.IsAny<long>())).ReturnsAsync(totalBankInfo);
        _mockCustomerBankInfoAppService.Setup(customerBankInfoAppService => customerBankInfoAppService.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>())).ReturnsAsync(It.IsAny<bool>());
        _mockPortfolioService.Setup(portfolioService => portfolioService.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()));

        // Action
        await _portfolioAppService.DepositAsync(customerIdTest, portfolioIdTest, amountTest).ConfigureAwait(false);

        // Assert
        _mockCustomerBankInfoAppService.Verify(customerBankInfoAppService => customerBankInfoAppService.GetAccountBalanceByCustomerIdAsync(It.IsAny<long>()), Times.Once);
        _mockCustomerBankInfoAppService.Verify(customerBankInfoAppService => customerBankInfoAppService.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        _mockPortfolioService.Verify(portfolioService => portfolioService.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
    }

    [Fact]
    public async void Should_Not_DepositPortfolioAsync_Throwing_ArgumentException()
    {
        // Arrage
        long customerIdTest = 1;
        long portfolioIdTest = 1;
        decimal amountTest = 20m;
        decimal totalBankInfo = 17.05m;

        _mockCustomerBankInfoAppService.Setup(customerBankInfoAppService => customerBankInfoAppService.GetAccountBalanceByCustomerIdAsync(It.IsAny<long>())).ReturnsAsync(totalBankInfo);

        // Action
        var action = () => _portfolioAppService.DepositAsync(customerIdTest, portfolioIdTest, amountTest);

        // Assert
        await action.Should().ThrowAsync<ArgumentException>();
        _mockCustomerBankInfoAppService.Verify(customerBankInfoAppService => customerBankInfoAppService.GetAccountBalanceByCustomerIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_ExecuteBuyOrderAsync_WithRelation_Between_PortfolioAndProduct_Successfully()
    {
        // Arrange
        long portfolioIdTest = 1;
        long productIdTest = 1;
        decimal amountTest = 17.05m;
        var portfolioProductTest = PortfolioProductFixture.GeneratePortfolioProductFixture();

        _mockPortfolioService.Setup(portfolioService => portfolioService.InvestAsync(It.IsAny<long>(), It.IsAny<decimal>()));
        _mockPortfolioProductService.Setup(portfolioProductAppService => portfolioProductAppService.GetPortfolioProductByIdsAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(portfolioProductTest);

        // Action
        await _portfolioAppService.ExecuteBuyOrderAsync(portfolioIdTest, productIdTest, amountTest).ConfigureAwait(false);

        // Assert
        _mockPortfolioService.Verify(portfolioService => portfolioService.InvestAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        _mockPortfolioProductService.Verify(portfolioProductAppService => portfolioProductAppService.GetPortfolioProductByIdsAsync(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_ExecuteBuyOrderAsync_WithNoRelation_Between_PortfolioAndProduct_Throwing_ArgumentException_Successfully()
    {
        // Arrange
        long portfolioIdTest = 1;
        long productIdTest = 1;
        decimal amountTest = 17.05m;
        var portfolioProductTest = PortfolioProductFixture.GeneratePortfolioProductFixture();

        _mockPortfolioService.Setup(portfolioService => portfolioService.InvestAsync(It.IsAny<long>(), It.IsAny<decimal>()));
        _mockPortfolioProductService.Setup(portfolioProductAppService => portfolioProductAppService.GetPortfolioProductByIdsAsync(It.IsAny<long>(), It.IsAny<long>())).Throws<ArgumentNullException>();
        _mockPortfolioProductService.Setup(portfolioProductAppService => portfolioProductAppService.Create(It.IsAny<PortfolioProduct>()));

        // Action
        await _portfolioAppService.ExecuteBuyOrderAsync(portfolioIdTest, productIdTest, amountTest).ConfigureAwait(false);

        // Assert       
        _mockPortfolioService.Verify(portfolioService => portfolioService.InvestAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        _mockPortfolioProductService.Verify(portfolioProductAppService => portfolioProductAppService.GetPortfolioProductByIdsAsync(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
        _mockPortfolioProductService.Verify(portfolioProductAppService => portfolioProductAppService.Create(It.IsAny<PortfolioProduct>()), Times.Once);
    }

    [Fact]
    public async void Shoul_ExecuteOrdersOfTheDayAsync_Successfully()
    {
        // Arrange
        var ordersResultTest = OrderResultFixture.GenerateListOrderResultFixture(10);
        var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();

        _mockOrderAppService.Setup(orderAppService => orderAppService.GetAllOrdersAsync()).ReturnsAsync(ordersResultTest);
        _mockOrderAppService.Setup(orderAppService => orderAppService.Update(It.IsAny<UpdateOrder>()));

        // Action
        await _portfolioAppService.ExecuteOrdersOfTheDayAsync();

        // Assert
        _mockOrderAppService.Verify(orderAppService => orderAppService.GetAllOrdersAsync(), Times.Once);
        _mockOrderAppService.Verify(orderAppService => orderAppService.Update(It.IsAny<UpdateOrder>()), Times.AtLeastOnce);
    }

    [Fact]
    public async void Should_ExecuteSellOrderAsync_WithNoMore_Available_Quotes_Successfully()
    {
        // Arrange
        int availableQuotesTest = 0;
        decimal amountTest = 17.05m;
        long portfolioIdTest = 1;
        long prodcutIdTest = 1;

        _mockPortfolioService.Setup(portfolioService => portfolioService.RedeemToPortfolioAsync(It.IsAny<long>(), It.IsAny<decimal>()));
        _mockOrderAppService.Setup(orderAppService => orderAppService.GetAvailableQuotes(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(availableQuotesTest);
        _mockPortfolioProductService.Setup(portfolioProductAppService => portfolioProductAppService.DeleteAsync(It.IsAny<long>(), It.IsAny<long>()));

        // Action
        await _portfolioAppService.ExecuteSellOrderAsync(portfolioIdTest, prodcutIdTest, amountTest).ConfigureAwait(false);

        // Assert
        _mockPortfolioService.Verify(portfolioService => portfolioService.RedeemToPortfolioAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        _mockOrderAppService.Verify(orderAppService => orderAppService.GetAvailableQuotes(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
        _mockPortfolioProductService.Verify(portfolioProductAppService => portfolioProductAppService.DeleteAsync(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_ExecuteSellOrderAsync_With_Available_Quotes_Successfully()
    {
        // Arrange
        int availableQuotesTest = 1;
        decimal amountTest = 17.05m;
        long portfolioIdTest = 1;
        long prodcutIdTest = 1;

        _mockPortfolioService.Setup(portfolioService => portfolioService.RedeemToPortfolioAsync(It.IsAny<long>(), It.IsAny<decimal>()));
        _mockOrderAppService.Setup(orderAppService => orderAppService.GetAvailableQuotes(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(availableQuotesTest);

        // Action
        await _portfolioAppService.ExecuteSellOrderAsync(portfolioIdTest, prodcutIdTest, amountTest).ConfigureAwait(false);

        // Assert
        _mockPortfolioService.Verify(portfolioService => portfolioService.RedeemToPortfolioAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        _mockOrderAppService.Verify(orderAppService => orderAppService.GetAvailableQuotes(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_GetPortfolioByIdAsync_Successfully()
    {
        // Arrange
        long idTest = 1;
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        portfolioTest.Id = idTest;

        _mockPortfolioService.Setup(portfolioService => portfolioService.GetPortfolioByIdAsync(It.IsAny<long>())).ReturnsAsync(portfolioTest);
        _mapper.Map<PortfolioResult>(portfolioTest);

        // Action
        var result = await _portfolioAppService.GetPortfolioByIdAsync(idTest).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        _mockPortfolioService.Verify(portfolioService => portfolioService.GetPortfolioByIdAsync(idTest), Times.Once);
    }

    [Fact]
    public async void Should_GetPortfoliosByCustomerIdAsync_Successfully()
    {
        // Arrange
        var listPortfoliosResultTest = PortfolioResultFixture.GenerateListPortfolioResultFixture(3);
        var listPortfoliosTest = PortfolioFixture.GenerateListPortfolioFixture(3);
        long customerIdTest = 1;

        _mockPortfolioService.Setup(portfolioService => portfolioService.GetPortfoliosByCustomerIdAsync(It.IsAny<long>())).ReturnsAsync(listPortfoliosTest);
        _mapper.Map<IEnumerable<PortfolioResult>>(listPortfoliosTest);

        // Action
        var result = await _portfolioAppService.GetPortfoliosByCustomerIdAsync(customerIdTest).ConfigureAwait(false);

        // Assert
        result.Should().HaveCountGreaterThanOrEqualTo(3);
        _mockPortfolioService.Verify(portfolioService => portfolioService.GetPortfoliosByCustomerIdAsync(customerIdTest), Times.Once);
    }

    [Fact]
    public async void Should_InvestAsync_Today_Succesfully()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        decimal amount = 17.05m;

        _mockProductAppService.Setup(productAppService => productAppService.GetProductUnitPriceByIdAsync(It.IsAny<long>())).ReturnsAsync(It.IsAny<decimal>());
        _mockOrderAppService.Setup(orderAppService => orderAppService.Create(It.IsAny<CreateOrder>())).Returns(It.IsAny<long>());

        // Action
        await _portfolioAppService.InvestAsync(createOrderTest).ConfigureAwait(false);

        // Assert
        _mockProductAppService.Verify(productAppService => productAppService.GetProductUnitPriceByIdAsync(It.IsAny<long>()), Times.Once);
        _mockOrderAppService.Verify(orderAppService => orderAppService.Create(It.IsAny<CreateOrder>()), Times.Once);
    }

    [Fact]
    public async void Should_InvestAsync_Tomorrow_Succesfully()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.LiquidatedAt = DateTime.Now.Date.AddDays(1);
        decimal amount = 17.05m;

        _mockProductAppService.Setup(productAppService => productAppService.GetProductUnitPriceByIdAsync(It.IsAny<long>())).ReturnsAsync(It.IsAny<decimal>());
        _mockOrderAppService.Setup(orderAppService => orderAppService.Create(It.IsAny<CreateOrder>())).Returns(It.IsAny<long>());

        // Action
        await _portfolioAppService.InvestAsync(createOrderTest).ConfigureAwait(false);

        // Assert
        _mockProductAppService.Verify(productAppService => productAppService.GetProductUnitPriceByIdAsync(It.IsAny<long>()), Times.Once);
        _mockOrderAppService.Verify(orderAppService => orderAppService.Create(It.IsAny<CreateOrder>()), Times.Once);
    }
    
    [Fact]
    public async void Should_RedeemToPortfolioAsync_Today_Succesfully()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        decimal amount = 17.05m;

        _mockProductAppService.Setup(productAppService => productAppService.GetProductUnitPriceByIdAsync(It.IsAny<long>())).ReturnsAsync(It.IsAny<decimal>());
        _mockOrderAppService.Setup(orderAppService => orderAppService.Create(It.IsAny<CreateOrder>())).Returns(It.IsAny<long>());

        // Action
        await _portfolioAppService.RedeemToPortfolioAsync(createOrderTest).ConfigureAwait(false);

        // Assert
        _mockProductAppService.Verify(productAppService => productAppService.GetProductUnitPriceByIdAsync(It.IsAny<long>()), Times.Once);
        _mockOrderAppService.Verify(orderAppService => orderAppService.Create(It.IsAny<CreateOrder>()), Times.Once);
    }

    [Fact]
    public async void Should_RedeemToPortfolioAsync_Tomorrow_Succesfully()
    {
        // Arrange
        var createOrderTest = CreateOrderFixture.GenerateCreateOrderFixture();
        createOrderTest.LiquidatedAt = DateTime.Now.Date.AddDays(1);
        decimal amount = 17.05m;

        _mockProductAppService.Setup(productAppService => productAppService.GetProductUnitPriceByIdAsync(It.IsAny<long>())).ReturnsAsync(It.IsAny<decimal>());
        _mockOrderAppService.Setup(orderAppService => orderAppService.Create(It.IsAny<CreateOrder>())).Returns(It.IsAny<long>());

        // Action
        await _portfolioAppService.RedeemToPortfolioAsync(createOrderTest).ConfigureAwait(false);

        // Assert
        _mockProductAppService.Verify(productAppService => productAppService.GetProductUnitPriceByIdAsync(It.IsAny<long>()), Times.Once);
        _mockOrderAppService.Verify(orderAppService => orderAppService.Create(It.IsAny<CreateOrder>()), Times.Once);
    }

    [Fact]
    public async void Should_WithdrawFromPortfolioAsync_Succedssfully()
    {
        // Arrange
        long customerIdTest = 1;
        long portfolioIdTest = 1;
        decimal amountTest = 17.05m;

        _mockPortfolioService.Setup(productAppService => productAppService.WithdrawFromPortfolioAsync(It.IsAny<long>(), It.IsAny<decimal>()));
        _mockCustomerBankInfoAppService.Setup(customerBankInfoAppService => customerBankInfoAppService.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()));

        // Action
        await _portfolioAppService.WithdrawFromPortfolioAsync(customerIdTest, portfolioIdTest, amountTest).ConfigureAwait(false);

        // Assert
        _mockPortfolioService.Verify(productAppService => productAppService.WithdrawFromPortfolioAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        _mockCustomerBankInfoAppService.Verify(customerBankInfoAppService => customerBankInfoAppService.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
    }
}