﻿using DomainModels.Models;
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
        result.Should().BeGreaterThanOrEqualTo(1);

        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Order>().Add(It.IsAny<Order>()), Times.Once);
    }

    [Fact]
    public async void Should_GetAllOrdersAsync_Successfully()
    {
        // Arrange
        var listorderTest = OrderFixture.GenerateListOrderFixture(3);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listorderTest);

        // Action
        var result = await _orderService.GetAllOrdersAsync().ConfigureAwait(false);

        // Arrange
        result.Should().NotBeNull();

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void ShouldNot_GetAllOrdersAsync_Throwing_ArgumentException()
    {
        // Arrange
        var listorderTest = OrderFixture.GenerateListOrderFixture(0);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listorderTest);

        // Action
        var action = () => _orderService.GetAllOrdersAsync();

        // Arrange
        await action.Should().ThrowAsync<ArgumentException>();

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_GetAvailableQuotesGetAvailableQuotes_Successfully()
    {
        // Arrange
        var listorderTest = OrderFixture.GenerateListOrderFixture(2);
        listorderTest[0].Direction = OrderDirection.Buy;
        listorderTest[0].Quotes = 1;
        listorderTest[1].Direction = OrderDirection.Sell;
        listorderTest[1].Quotes = 1;

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listorderTest);

        // Action
        var result = await _orderService.GetAvailableQuotes(It.IsAny<long>(), It.IsAny<long>()).ConfigureAwait(false);

        // Arrange
        result.Should().BeGreaterThanOrEqualTo(0);

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
        var result = await _orderService.GetOrderByIdAsync(It.IsAny<long>());

        // Arrange
        result.Should().NotBeNull();

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_NotGetOrderByIdAsync_Throwing_ArgumentNullException()
    {
        // Arrange
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<Order>>(), default))
            .ReturnsAsync(It.IsAny<Order>());

        // Action
        var action = () => _orderService.GetOrderByIdAsync(It.IsAny<long>());

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>();

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_GetOrderByPorfolioIdAndProductIdAsync_Successfully()
    {
        // Arrange
        var listorderTest = OrderFixture.GenerateListOrderFixture(2);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listorderTest);

        // Action
        var result = await _orderService.GetOrderByPorfolioIdAndProductIdAsync(It.IsAny<long>(), It.IsAny<long>()).ConfigureAwait(false);

        // Arrange
        result.Should().NotBeNull();

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_NotGetOrderByPorfolioIdAndProductIdAsync__Throwing_ArgumentNullException()
    {
        // Arrange
        var listorderTest = OrderFixture.GenerateListOrderFixture(0);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listorderTest);

        // Action
        var action = () => _orderService.GetOrderByPorfolioIdAndProductIdAsync(It.IsAny<long>(), It.IsAny<long>());

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>();

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
        var listorderTest = OrderFixture.GenerateListOrderFixture(3);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listorderTest);

        // Action
        var result = await _orderService.GetOrdersByPortfolioIdAsync(It.IsAny<long>()).ConfigureAwait(false);

        // Arrange
        result.Should().NotBeNull();

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
        var listorderTest = OrderFixture.GenerateListOrderFixture(0);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listorderTest);

        // Action
        var action = () => _orderService.GetOrdersByPortfolioIdAsync(It.IsAny<long>());

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>();

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
        var listorderTest = OrderFixture.GenerateListOrderFixture(3);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listorderTest);

        // Action
        var result = await _orderService.GetOrdersByProductIdAsync(It.IsAny<long>()).ConfigureAwait(false);

        // Arrange
        result.Should().NotBeNull();

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
        var listorderTest = OrderFixture.GenerateListOrderFixture(0);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Order, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Order>, IIncludableQueryable<Order, object>>>()))
            .Returns(It.IsAny<IMultipleResultQuery<Order>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Order>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Order>>(), default))
            .ReturnsAsync(listorderTest);

        // Action
        var action = () => _orderService.GetOrdersByProductIdAsync(It.IsAny<long>());

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>();

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