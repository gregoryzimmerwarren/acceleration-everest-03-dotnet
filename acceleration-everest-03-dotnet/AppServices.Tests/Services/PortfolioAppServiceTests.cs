using AppModels.Customers;
using AppModels.Portfolios;
using AppModels.PortfoliosProducts;
using AppServices.Interfaces;
using AppServices.Services;
using AppServices.Tests.Fixtures.Customers;
using AppServices.Tests.Fixtures.Portfolios;
using AppServices.Tests.Fixtures.PortfoliosProducts;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;

namespace AppServices.Tests.Services;

public class PortfolioAppServiceTests
{
    private readonly Mock<ICustomerBankInfoAppService> _mockCustomerBankInfoAppService;
    private readonly Mock<IPortfolioProductAppService> _mockPortfolioProductAppService;
    private readonly Mock<IPortfolioProductService> _mockPortfolioProductService;
    private readonly PortfolioProductAppService _portfolioProductAppService;
    private readonly Mock<IProductAppService> _mockProductAppService;
    private readonly Mock<IPortfolioService> _mockPortfolioService;
    private readonly Mock<IOrderAppService> _mockOrderAppService;
    private readonly PortfolioAppService _portfolioAppService;
    private readonly Mock<IOrderService> _mockOrderService;
    private readonly OrderAppService _orderAppService;
    private readonly IMapper _mapper;

    public PortfolioAppServiceTests( )
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Portfolio, PortfolioResult>();
            cfg.CreateMap<Portfolio, PortfolioResultForOthersDtos>();
            cfg.CreateMap<CreatePortfolio, Portfolio>();
            cfg.CreateMap<CreatePortfolioProduct, PortfolioProduct>();
        });
        _mapper = config.CreateMapper();
        _mockOrderService = new Mock<IOrderService>();
        _mockOrderAppService = new Mock<IOrderAppService>();
        _mockPortfolioService = new Mock<IPortfolioService>();
        _mockProductAppService = new Mock<IProductAppService>();
        _mockPortfolioProductService = new Mock<IPortfolioProductService>();
        _mockPortfolioProductAppService = new Mock<IPortfolioProductAppService>();
        _mockCustomerBankInfoAppService = new Mock<ICustomerBankInfoAppService>();
        _orderAppService = new OrderAppService(_mockProductAppService.Object, _mockOrderService.Object, _mapper);
        _portfolioProductAppService = new PortfolioProductAppService(_mockPortfolioProductService.Object, _mapper);
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
        long customerIdTest = 1;
        long portfolioIdTest = 1;
        decimal amountTest = 17.05m;
        decimal totalBankInfo = 20m;

        _mockCustomerBankInfoAppService.Setup(customerBankInfoAppService => customerBankInfoAppService.GetTotalByCustomerIdAsync(It.IsAny<long>())).ReturnsAsync(totalBankInfo);
        _mockCustomerBankInfoAppService.Setup(customerBankInfoAppService => customerBankInfoAppService.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>())).ReturnsAsync(It.IsAny<bool>());
        _mockPortfolioService.Setup(portfolioService => portfolioService.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()));

        // Action
        await _portfolioAppService.DepositAsync(customerIdTest, portfolioIdTest, amountTest).ConfigureAwait(false);

        // Assert
        _mockCustomerBankInfoAppService.Verify(customerBankInfoAppService => customerBankInfoAppService.GetTotalByCustomerIdAsync(It.IsAny<long>()), Times.Once);
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

        _mockCustomerBankInfoAppService.Setup(customerBankInfoAppService => customerBankInfoAppService.GetTotalByCustomerIdAsync(It.IsAny<long>())).ReturnsAsync(totalBankInfo);

        // Action
        var action = () => _portfolioAppService.DepositAsync(customerIdTest, portfolioIdTest, amountTest);

        // Assert
        await action.Should().ThrowAsync<ArgumentException>();
        _mockCustomerBankInfoAppService.Verify(customerBankInfoAppService => customerBankInfoAppService.GetTotalByCustomerIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_ExecuteBuyOrderAsync_WithNoRelation_Between_PortfolioAndProduct_Successfully()
    {
        // Arrange
        long portfolioIdTest = 1;
        long productIdTest = 1;
        var portfolioProductResultTest = PortfolioProductResultFixture.GeneratePortfolioProductResultFixture();
        decimal amountTest = 17.05m;

        _mockPortfolioService.Setup(portfolioService => portfolioService.InvestAsync(It.IsAny<long>(), It.IsAny<decimal>()));
        _mockPortfolioProductAppService.Setup(portfolioProductAppService => portfolioProductAppService.GetPortfolioProductByIdsAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(portfolioProductResultTest);

        // Action
        await _portfolioAppService.ExecuteBuyOrderAsync(portfolioIdTest, productIdTest, amountTest).ConfigureAwait(false);
        await _portfolioProductAppService.GetPortfolioProductByIdsAsync(portfolioIdTest, productIdTest).ConfigureAwait(false);

        // Assert
        _mockPortfolioService.Verify(portfolioService => portfolioService.InvestAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        _mockPortfolioProductAppService.Verify(portfolioProductAppService => portfolioProductAppService.GetPortfolioProductByIdsAsync(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_ExecuteBuyOrderAsync_WithRelation_Between_PortfolioAndProduct_Successfully_Throwing_ArgumentException()
    {
        // Arrange
        long idTest = 2;
        long portfolioIdTest = 1;
        long productIdTest = 1;
        var createPortfolioProductTest = CreatePortfolioProductFixture.GenerateCreatePortfolioProductFixture();
        decimal amountTest = 17.05m;

        _mockPortfolioService.Setup(portfolioService => portfolioService.InvestAsync(It.IsAny<long>(), It.IsAny<decimal>()));
        _mockPortfolioProductAppService.Setup(portfolioProductAppService => portfolioProductAppService.GetPortfolioProductByIdsAsync(It.IsAny<long>(), It.IsAny<long>())).Throws<ArgumentNullException>();
        _mockPortfolioProductAppService.Setup(portfolioProductAppService => portfolioProductAppService.Create(It.IsAny<CreatePortfolioProduct>()));

        // Action
        await _portfolioAppService.ExecuteBuyOrderAsync(portfolioIdTest, productIdTest, amountTest).ConfigureAwait(false);
        await _portfolioProductAppService.GetPortfolioProductByIdsAsync(idTest, idTest);
        _portfolioProductAppService.Create(createPortfolioProductTest);

        // Assert       
        _mockPortfolioService.Verify(portfolioService => portfolioService.InvestAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        _mockPortfolioProductAppService.Verify(portfolioProductAppService => portfolioProductAppService.GetPortfolioProductByIdsAsync(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
        _mockPortfolioProductAppService.Verify(portfolioProductAppService => portfolioProductAppService.Create(It.IsAny<CreatePortfolioProduct>()), Times.Once);
    }

    //[Fact]
    //public async void Shoul_ExecuteOrdersOfTheDayAsync_Successfully()
    //{
    //    // Arrange
    //    var ordersResultTest = OrderResultFixture.GenerateListOrderResultFixture(10);
    //    var updateOrderTest = UpdateOrderFixture.GenerateUpdateOrderFixture();

    //    _mockOrderAppService.Setup(orderAppService => orderAppService.GetAllOrdersAsync()).ReturnsAsync(ordersResultTest);
    //    _mockPortfolioAppService.Setup(portfolioAppService => portfolioAppService.ExecuteBuyOrderAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<decimal>()));
    //    _mockPortfolioAppService.Setup(portfolioAppService => portfolioAppService.ExecuteSellOrderAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<decimal>()));
    //    _mockOrderAppService.Setup(orderAppService => orderAppService.Update(It.IsAny<UpdateOrder>()));

    //    // Action
    //    await _portfolioAppService.ExecuteOrdersOfTheDayAsync();
    //    foreach (var order in ordersResultTest)
    //    {
    //        if (order.LiquidatedAt.Date == DateTime.Now.Date && order.WasExecuted == false)
    //        {
    //            if (order.Direction == "Buy")
    //            {
    //                await _portfolioAppService.ExecuteBuyOrderAsync(order.Product.Id, order.Product.Id, order.NetValue).ConfigureAwait(false);
    //            }
    //            else
    //            {
    //                await _portfolioAppService.ExecuteSellOrderAsync(order.Product.Id, order.Product.Id, order.NetValue).ConfigureAwait(false);
    //            }

    //            _mockOrderAppService.Update(new UpdateOrder(order.Id, order.Quotes, order.NetValue, order.Direction, order.WasExecuted, order.LiquidatedAt, order.Portfolio.Id, order.Product.Id));
    //        }
    //    }


    //    // Assert
    //    _mockOrderAppService.Verify(orderAppService => orderAppService.GetAllOrdersAsync(), Times.Once);
    //    _mockPortfolioAppService.Verify(portfolioAppService => portfolioAppService.ExecuteBuyOrderAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<decimal>()), Times.AtLeastOnce);
    //    _mockPortfolioAppService.Verify(portfolioAppService => portfolioAppService.ExecuteSellOrderAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<decimal>()), Times.AtLeastOnce);
    //    _mockOrderAppService.Verify(orderAppService => orderAppService.Update(It.IsAny<UpdateOrder>()), Times.AtLeastOnce);
    //}

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
        _mockPortfolioProductAppService.Setup(portfolioProductAppService => portfolioProductAppService.DeleteAsync(It.IsAny<long>(), It.IsAny<long>()));

        // Action
        await _portfolioAppService.ExecuteSellOrderAsync(portfolioIdTest, prodcutIdTest, amountTest).ConfigureAwait(false);
        await _orderAppService.GetAvailableQuotes(portfolioIdTest, prodcutIdTest).ConfigureAwait(false);
        await _portfolioProductAppService.DeleteAsync(portfolioIdTest, prodcutIdTest).ConfigureAwait(false);

        // Assert
        _mockPortfolioService.Verify(portfolioService => portfolioService.RedeemToPortfolioAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        _mockOrderAppService.Verify(orderAppService => orderAppService.GetAvailableQuotes(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
        _mockPortfolioProductAppService.Verify(portfolioProductAppService => portfolioProductAppService.DeleteAsync(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
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
        await _orderAppService.GetAvailableQuotes(portfolioIdTest, prodcutIdTest).ConfigureAwait(false);

        // Assert
        _mockPortfolioService.Verify(portfolioService => portfolioService.RedeemToPortfolioAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
        _mockOrderAppService.Verify(orderAppService => orderAppService.GetAvailableQuotes(It.IsAny<long>(), It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_GetAllPortfoliosAsync_Successfully()
    {
        // Arrange
        var listPortfoliosResultTest = PortfolioResultFixture.GenerateListPortfolioResultFixture(3);
        var listPortfoliosTest = PortfolioFixture.GenerateListPortfolioFixture(3);

        _mockPortfolioService.Setup(portfolioService => portfolioService.GetAllPortfoliosAsync()).ReturnsAsync(listPortfoliosTest);
        _mapper.Map<IEnumerable<PortfolioResult>>(listPortfoliosTest);

        // Action
        var result = await _portfolioAppService.GetAllPortfoliosAsync().ConfigureAwait(false);

        result.Should().HaveCountGreaterThanOrEqualTo(3);
        _mockPortfolioService.Verify(portfolioService => portfolioService.GetAllPortfoliosAsync(), Times.Once);
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
}