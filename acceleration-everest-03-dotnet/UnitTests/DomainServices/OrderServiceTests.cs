using DomainModels.Models;
using DomainServices.Services;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.CrossCutting.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;
using UnitTests.Fixtures.Orders;

namespace UnitTests.DomainServices;

public class OrderServiceTests
{
    private readonly Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>> _mockRepositoryFactory;
    private readonly Mock<IUnitOfWork<WarrenEverestDotnetDbContext>> _mockUnitOfWork;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork<WarrenEverestDotnetDbContext>>();
        _mockRepositoryFactory = new Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>>();
        _orderService = new OrderService(_mockUnitOfWork.Object, _mockRepositoryFactory.Object);
    }

    [Fact]
    public void Should_CreateAsync_Successfully()
    {
        // Arrange
        var orderTest = OrderFixture.GenerateOrderFixture();

        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Order>().Add(It.IsAny<Order>()));

        // Action
        var result = _orderService.Create(orderTest);

        // Arrange
        result.Should().Be(1);

        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Order>().Add(It.IsAny<Order>()), Times.Once);
    }

    [Fact]
    public async void Should_GetAllOrdersAsync_Successfully()
    {
        // Arrange
        var listOrderTest = OrderFixture.GenerateListOrderFixture(3);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listOrderTest);

        // Action
        var result = await _orderService.GetAllOrdersAsync().ConfigureAwait(false);

        // Arrange
        result.Should().BeEquivalentTo(listOrderTest);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void ShouldNot_GetAllOrdersAsync_Throwing_ArgumentException()
    {
        // Arrange
        var listOrderTest = OrderFixture.GenerateListOrderFixture(0);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listOrderTest);

        // Action
        var action = () => _orderService.GetAllOrdersAsync();

        // Arrange
        await action.Should().ThrowAsync<ArgumentException>("No order found");

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_GetAvailableQuotesGetAvailableQuotes_Successfully()
    {
        // Arrange
        long portfolioIdTest = 1;

        long productIdTest = 1;

        var listOrderTest = OrderFixture.GenerateListOrderFixture(2);
        listOrderTest[0].Direction = OrderDirection.Buy;
        listOrderTest[0].Quotes = 1;
        listOrderTest[1].Direction = OrderDirection.Sell;
        listOrderTest[1].Quotes = 1;

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listOrderTest);

        // Action
        var result = await _orderService.GetAvailableQuotes(portfolioIdTest, productIdTest).ConfigureAwait(false);

        // Arrange
        result.Should().Be(0);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_GetOrderByIdAsync_Successfully()
    {
        // Arrange
        var orderTest = OrderFixture.GenerateOrderFixture();

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<Order>>(), default))
            .ReturnsAsync(orderTest);

        // Action
        var result = await _orderService.GetOrderByIdAsync(orderTest.Id);

        // Arrange
        result.Should().Be(orderTest);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void ShouldNot_GetOrderByIdAsync_Throwing_ArgumentNullException()
    {
        // Arrange
        long orderIdtest = 1;

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<Order>>(), default))
            .ReturnsAsync(It.IsAny<Order>());

        // Action
        var action = () => _orderService.GetOrderByIdAsync(orderIdtest);

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>($"No order found for Id: {orderIdtest}");

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_GetOrdersByPorfolioIdAndProductIdAsync_Successfully()
    {
        // Arrange
        long portfolioIdTest = 1;

        long productIdTest = 1;

        var listOrderTest = OrderFixture.GenerateListOrderFixture(2);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listOrderTest);

        // Action
        var result = await _orderService.GetOrdersByPorfolioIdAndProductIdAsync(portfolioIdTest, productIdTest).ConfigureAwait(false);

        // Arrange
        result.Should().BeEquivalentTo(listOrderTest);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void ShouldNot_GetOrdersByPorfolioIdAndProductIdAsync__Throwing_ArgumentNullException()
    {
        // Arrange
        long portfolioIdTest = 1;

        long productIdTest = 1;

        var listOrderTest = OrderFixture.GenerateListOrderFixture(0);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listOrderTest);

        // Action
        var action = () => _orderService.GetOrdersByPorfolioIdAndProductIdAsync(portfolioIdTest, productIdTest);

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>($"No order was found between portfolio Id: {portfolioIdTest} and product Id: {productIdTest}");

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_GetOrdersByPortfolioIdAsync_Successfully()
    {
        // Arrange
        long portfolioIdTest = 1;
        
        var listOrderTest = OrderFixture.GenerateListOrderFixture(3);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listOrderTest);

        // Action
        var result = await _orderService.GetOrdersByPortfolioIdAsync(portfolioIdTest).ConfigureAwait(false);

        // Arrange
        result.Should().BeEquivalentTo(listOrderTest);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void ShouldNot_GetOrdersByPortfolioIdAsync_Throwing_ArgumentNullException()
    {
        // Arrange
        long portfolioIdTest = 1;

        var listOrderTest = OrderFixture.GenerateListOrderFixture(0);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listOrderTest);

        // Action
        var action = () => _orderService.GetOrdersByPortfolioIdAsync(portfolioIdTest);

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>($"No order found for portfolio Id: {portfolioIdTest}");

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_GetOrdersByProductIdAsync_Successfully()
    {
        // Arrange
        long productIdTest = 1;

        var listOrderTest = OrderFixture.GenerateListOrderFixture(3);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listOrderTest);

        // Action
        var result = await _orderService.GetOrdersByProductIdAsync(productIdTest).ConfigureAwait(false);

        // Arrange
        result.Should().BeEquivalentTo(listOrderTest);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void ShouldNot_GetOrdersByProductIdAsync_Throwing_ArgumentNullException()
    {
        // Arrange
        long productIdTest = 1;

        var listOrderTest = OrderFixture.GenerateListOrderFixture(0);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listOrderTest);

        // Action
        var action = () => _orderService.GetOrdersByProductIdAsync(productIdTest);

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>($"No order found for product Id: {productIdTest}");

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public void Should_Update_Successfully()
    {
        // Arrange
        var orderTest = OrderFixture.GenerateOrderFixture();

        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Order>().Update(It.IsAny<Order>()));

        // Action
        _orderService.Update(orderTest);

        // Arrange
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Order>().Update(It.IsAny<Order>()), Times.Once);
    }
}