using DomainModels.Models;
using DomainServices.Services;
using DomainServices.Tests.Fixtures;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace DomainServices.Tests.Services;

public class CustomerBankInfoServiceTests
{
    private readonly Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>> _mockRepositoryFactory;
    private readonly Mock<IUnitOfWork<WarrenEverestDotnetDbContext>> _mockUnitOfWork;
    private readonly CustomerBankInfoService _customerBankInfoService;

    public CustomerBankInfoServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork<WarrenEverestDotnetDbContext>>();
        _mockRepositoryFactory = new Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>>();
        _customerBankInfoService = new CustomerBankInfoService(_mockUnitOfWork.Object, _mockRepositoryFactory.Object);
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
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>().MultipleResultQuery()
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>())).Returns(It.IsAny<IMultipleResultQuery<CustomerBankInfo>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<CustomerBankInfo>>(), default)).ReturnsAsync(listcustomerBankInfoTest);

        // Action
        var result = await _customerBankInfoService.GetAllCustomersBankInfoAsync().ConfigureAwait(false);

        // Arrange
        result.Should().NotBeNull();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>().MultipleResultQuery()
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<CustomerBankInfo>>(), default), Times.Once);
    }

    [Fact]
    public async void ShouldNot_GetAllCustomersBankInfoAsync_Throwing_ArgumentNullException()
    {
        // Arrange
        var listcustomerBankInfoTest = CustomerBankInfoFixture.GenerateListCustomerBankInfoFixture(0);
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>().MultipleResultQuery()
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>())).Returns(It.IsAny<IMultipleResultQuery<CustomerBankInfo>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<CustomerBankInfo>>(), default)).ReturnsAsync(It.IsAny<IList<CustomerBankInfo>>());

        // Action
        var action = () => _customerBankInfoService.GetAllCustomersBankInfoAsync();

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>().MultipleResultQuery()
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<CustomerBankInfo>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<CustomerBankInfo>>(), default), Times.Once);
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
    public async void Should_WithdrawAsync_Throwing_ArgumentException_When_Amount_BiggerThan_AccountBalance()
    {
        // Arrange
        decimal amountTest = 20m;
        var customerBankInfoTest = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();
        customerBankInfoTest.AccountBalance = 17.05m;
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>())).Returns(It.IsAny<IQuery<CustomerBankInfo>>());
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<CustomerBankInfo>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default)).ReturnsAsync(customerBankInfoTest);

        // Action
        var action = () => _customerBankInfoService.WithdrawAsync(customerBankInfoTest.CustomerId, amountTest);

        // Arrange
        await action.Should().ThrowAsync<ArgumentException>();
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<CustomerBankInfo, bool>>>())
        .Include(It.IsAny<Func<IQueryable<CustomerBankInfo>, IIncludableQueryable<CustomerBankInfo, object>>>()), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<CustomerBankInfo>().SingleOrDefaultAsync(It.IsAny<IQuery<CustomerBankInfo>>(), default), Times.Once);
    }
}