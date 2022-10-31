﻿using DomainModels.Models;
using DomainServices.Services;
using DomainServices.Tests.Fixtures;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace DomainServices.Tests.Services;

public class CustomerBankInfoServiceTests
{
    private readonly Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>> _mockrepositoryFactory;
    private readonly Mock<IUnitOfWork<WarrenEverestDotnetDbContext>> _mockUnitOfWork;
    private readonly CustomerBankInfoService _customerBankInfoService;

    public CustomerBankInfoServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork<WarrenEverestDotnetDbContext>>();
        _mockrepositoryFactory = new Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>>();
        _customerBankInfoService = new CustomerBankInfoService(_mockUnitOfWork.Object, _mockrepositoryFactory.Object);
    }

    [Fact]
    public void Should_Create_Successfully()
    {
        // Arrange
        long customerIdTest = 1;
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().Add(It.IsAny<CustomerBankInfo>()));

        // Action
        _customerBankInfoService.Create(customerIdTest);

        // Arrange
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().Add(It.IsAny<CustomerBankInfo>()), Times.Once);
    }

    [Fact]
    public async void Should_DeleteAsync_Successfully()
    {
        // Arrange        
        var customerBankInfoTest = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();
        customerBankInfoTest.Customer = CustomerFixture.GenerateCustomerFixture();
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default)).ReturnsAsync(customerBankInfoTest);
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().Remove(It.IsAny<CustomerBankInfo>()));

        // Action
        await _customerBankInfoService.DeleteAsync(customerBankInfoTest.CustomerId).ConfigureAwait(false);

        // Arrange
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>()), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().Remove(It.IsAny<CustomerBankInfo>()), Times.Once);
    }

    [Fact]
    public async void Should_DepositAsync_Successfully()
    {
        // Arrange
        decimal amountTest = 17.05m;
        var customerBankInfoTest = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default)).ReturnsAsync(customerBankInfoTest);
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().Update(It.IsAny<CustomerBankInfo>()));

        // Action
        await _customerBankInfoService.DepositAsync(customerBankInfoTest.CustomerId, amountTest).ConfigureAwait(false);

        // Arrange
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>()), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().Update(It.IsAny<CustomerBankInfo>()), Times.Once);
    }

    [Fact]
    public async void Should_GetAllCustomersBankInfoAsync_Successfully()
    {
        // Arrange
        var listcustomerBankInfoTest = CustomerBankInfoFixture.GenerateListCustomerBankInfoFixture(3);
        _mockrepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>()
        .MultipleResultQuery().Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());
        _mockrepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>().SearchAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default))
            .ReturnsAsync(It.IsAny<IList<CustomerBankInfo>>());

        // Action
        var result = await _customerBankInfoService.GetAllCustomersBankInfoAsync().ConfigureAwait(false);

        // Arrange
        result.Should().NotBeNull();
        _mockrepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>()
        .MultipleResultQuery().Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>()), Times.Once);
        _mockrepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>().SearchAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);
    }
    
    [Fact]
    public async void Should_GetAllCustomersBankInfoAsync_Throwing_ArgumentNullException()
    {
        // Arrange
        var listcustomerBankInfoTest = CustomerBankInfoFixture.GenerateListCustomerBankInfoFixture(0);
        _mockrepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>()
        .MultipleResultQuery().Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());
        _mockrepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>().SearchAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default))
            .ReturnsAsync(It.IsAny<IList<CustomerBankInfo>>());

        // Action
        var action = () => _customerBankInfoService.GetAllCustomersBankInfoAsync();

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>();
        _mockrepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>()
        .MultipleResultQuery().Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>()), Times.Once);
        _mockrepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>().SearchAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);
    }
    
    [Fact]
    public async void Should_GetCustomerBankInfoByCustomerIdAsync_Successfully()
    {
        // Arrange
        var customerBankInfoTest = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default))
            .ReturnsAsync(customerBankInfoTest);

        // Action
        var result = await _customerBankInfoService.GetCustomerBankInfoByCustomerIdAsync(customerBankInfoTest.CustomerId);

        // Arrange
        result.Should().NotBeNull();
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>()), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);
    }
    
    [Fact]
    public async void Should_GetCustomerBankInfoByCustomerIdAsync_Throwing_ArgumentNullException()
    {
        // Arrange
        long idTest = 1;
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default))
            .ReturnsAsync(It.IsAny<CustomerBankInfo>());

        // Action
        var action = () => _customerBankInfoService.GetCustomerBankInfoByCustomerIdAsync(idTest);

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>();
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>()), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);
    }
    
    
    [Fact]
    public async void Should_GetAccountBalanceByCustomerIdAsync_Successfully()
    {
        // Arrange
        var customerBankInfoTest = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default))
            .ReturnsAsync(customerBankInfoTest);

        // Action
        var result = await _customerBankInfoService.GetAccountBalanceByCustomerIdAsync(customerBankInfoTest.CustomerId);

        // Arrange
        result.Should().Be(customerBankInfoTest.AccountBalance);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>()), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);
    }
    
    [Fact]
    public async void Should_GetAccountBalanceByCustomerIdAsync_Throwing_ArgumentNullException()
    {
        // Arrange
        long idTest = 1;
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default))
            .ReturnsAsync(It.IsAny<CustomerBankInfo>());

        // Action
        var action = () => _customerBankInfoService.GetAccountBalanceByCustomerIdAsync(idTest);

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>();
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>()), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_WithdrawAsync_Successfully()
    {
        // Arrange
        decimal amountTest = 17.05m;
        var customerBankInfoTest = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();
        customerBankInfoTest.AccountBalance = 20m;
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default)).ReturnsAsync(customerBankInfoTest);
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().Update(It.IsAny<CustomerBankInfo>()));

        // Action
        await _customerBankInfoService.WithdrawAsync(customerBankInfoTest.CustomerId, amountTest).ConfigureAwait(false);

        // Arrange
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>()), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().Update(It.IsAny<CustomerBankInfo>()), Times.Once);
    }

    [Fact]
    public async void Should_WithdrawAsync_Throwing_ArgumentNullException()
    {
        // Arrange
        long idTest = 2;
        decimal amountTest = 17.05m;
        var customerBankInfoTest = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();
        customerBankInfoTest.AccountBalance = 20m;
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default)).ReturnsAsync(It.IsAny<CustomerBankInfo>());

        // Action
        var action = () => _customerBankInfoService.WithdrawAsync(idTest, amountTest);

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>();
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>()), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_WithdrawAsync_Throwing_ArgumentException()
    {
        // Arrange
        decimal amountTest = 20m;
        var customerBankInfoTest = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();
        customerBankInfoTest.AccountBalance = 17.05m;
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default)).ReturnsAsync(It.IsAny<CustomerBankInfo>());

        // Action
        var action = () => _customerBankInfoService.WithdrawAsync(customerBankInfoTest.CustomerId, amountTest);

        // Arrange
        await action.Should().ThrowAsync<ArgumentException>();
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>()), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);
    }

}