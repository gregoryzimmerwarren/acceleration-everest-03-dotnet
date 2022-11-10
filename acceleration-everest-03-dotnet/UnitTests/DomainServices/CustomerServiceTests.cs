using DomainModels.Models;
using DomainServices.Services;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;
using UnitTests.Fixtures.Customers;

namespace UnitTests.DomainServices;

public class CustomerServiceTests
{
    private readonly Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>> _mockRepositoryFactory;
    private readonly Mock<IUnitOfWork<WarrenEverestDotnetDbContext>> _mockUnitOfWork;
    private readonly CustomerService _customerService;

    public CustomerServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork<WarrenEverestDotnetDbContext>>();
        _mockRepositoryFactory = new Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>>();
        _customerService = new CustomerService(_mockUnitOfWork.Object, _mockRepositoryFactory.Object);
    }

    [Fact]
    public async void Should_CreateAsync_Successfully()
    {
        // Arrange
        var customerTest = CustomerFixture.GenerateCustomerFixture();

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().AnyAsync(It.IsAny<Expression<Func<Customer, bool>>>(), default)).ReturnsAsync(false);
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Customer>().Add(It.IsAny<Customer>()));

        // Action
        var result = await _customerService.CreateAsync(customerTest).ConfigureAwait(false);

        // Arrange
        result.Should().BeGreaterThanOrEqualTo(1);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().AnyAsync(It.IsAny<Expression<Func<Customer, bool>>>(), default), Times.Exactly(2));
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Customer>().Add(It.IsAny<Customer>()), Times.Once);
    }

    [Fact]
    public async void Should_NotCreateAsync_Throwing_ArgumentException_When_EmailAlreadyExist()
    {
        // Arrange
        var customerTest = CustomerFixture.GenerateCustomerFixture();

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>()
        .AnyAsync(customer => customer.Email == customerTest.Email && customer.Id != customerTest.Id, default)).ReturnsAsync(true);
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().SingleResultQuery()
        .AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(It.IsAny<IQuery<Customer>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().SingleOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default)).ReturnsAsync(customerTest);

        // Action
        var action = () => _customerService.CreateAsync(customerTest);

        // Arrange
        await action.Should().ThrowAsync<ArgumentException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().AnyAsync(It.IsAny<Expression<Func<Customer, bool>>>(), default), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().SingleOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_NotCreateAsync_Throwing_ArgumentException_When_CpfAlreadyExist()
    {
        // Arrange
        var customerTest = CustomerFixture.GenerateCustomerFixture();

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>()
        .AnyAsync(customer => customer.Email == customerTest.Email && customer.Id != customerTest.Id, default)).ReturnsAsync(false);
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>()
        .AnyAsync(customer => customer.Cpf == customerTest.Cpf && customer.Id != customerTest.Id, default)).ReturnsAsync(true);
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().SingleResultQuery()
        .AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(It.IsAny<IQuery<Customer>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().SingleOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default)).ReturnsAsync(customerTest);

        // Action
        var action = () => _customerService.CreateAsync(customerTest);

        // Arrange
        await action.Should().ThrowAsync<ArgumentException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().AnyAsync(It.IsAny<Expression<Func<Customer, bool>>>(), default), Times.Exactly(2));
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().SingleOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_DeleteAsync_Successfully()
    {
        // Arrange
        var customerTest = CustomerFixture.GenerateCustomerFixture();

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>>())).Returns(It.IsAny<IQuery<Customer>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().SingleOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default)).ReturnsAsync(customerTest);
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Customer>().Remove(customerTest));

        // Action
        await _customerService.DeleteAsync(It.IsAny<long>()).ConfigureAwait(false);

        // Arrange
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().SingleOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Customer>().Remove(It.IsAny<Customer>()), Times.Once);
    }

    [Fact]
    public async void Should_GetAllCustomersAsync_Successfully()
    {
        // Arrange
        var listcustomerTest = CustomerFixture.GenerateListCustomerFixture(3);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().MultipleResultQuery()
        .Include(It.IsAny<Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>>())).Returns(It.IsAny<IMultipleResultQuery<Customer>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Customer>>(), default)).ReturnsAsync(listcustomerTest);

        // Action
        var result = await _customerService.GetAllCustomersAsync().ConfigureAwait(false);

        // Arrange
        result.Should().NotBeNull();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().MultipleResultQuery()
        .Include(It.IsAny<Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Customer>>(), default), Times.Once);
    }

    [Fact]
    public async void ShouldNot_GetAllCustomersAsync_Throwing_ArgumentException()
    {
        // Arrange
        var listcustomerTest = CustomerFixture.GenerateListCustomerFixture(0);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().MultipleResultQuery()
        .Include(It.IsAny<Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>>())).Returns(It.IsAny<IMultipleResultQuery<Customer>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Customer>>(), default)).ReturnsAsync(listcustomerTest);

        // Action
        var action = () => _customerService.GetAllCustomersAsync();

        // Arrange
        await action.Should().ThrowAsync<ArgumentException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().MultipleResultQuery()
        .Include(It.IsAny<Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Customer>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_GetCustomerByIdAsync_Successfully()
    {
        // Arrange
        var customerTest = CustomerFixture.GenerateCustomerFixture();

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>>())).Returns(It.IsAny<IQuery<Customer>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().SingleOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default))
            .ReturnsAsync(customerTest);

        // Action
        var result = await _customerService.GetCustomerByIdAsync(It.IsAny<long>());

        // Arrange
        result.Should().NotBeNull();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().SingleOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_NotGetCustomerByIdAsync_Throwing_ArgumentNullException()
    {
        // Arrange
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>>())).Returns(It.IsAny<IQuery<Customer>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().SingleOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default))
            .ReturnsAsync(It.IsAny<Customer>());

        // Action
        var action = () => _customerService.GetCustomerByIdAsync(It.IsAny<long>());

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().SingleOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_UpdateAsync_Successfully()
    {
        // Arrange
        var customerTest = CustomerFixture.GenerateCustomerFixture();

        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Customer>().Any(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(true);
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().AnyAsync(It.IsAny<Expression<Func<Customer, bool>>>(), default)).ReturnsAsync(false);
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Customer>().Update(It.IsAny<Customer>()));

        // Action
        await _customerService.UpdateAsync(customerTest).ConfigureAwait(false);

        // Arrange
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Customer>().Any(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().AnyAsync(It.IsAny<Expression<Func<Customer, bool>>>(), default), Times.Exactly(2));
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Customer>().Update(It.IsAny<Customer>()), Times.Once);
    }

    [Fact]
    public async void Should_NotUpdateAsync_Throwing_ArgumentNullException_When_NoCustomerFound_ForThatId()
    {
        // Arrange
        var customerTest = CustomerFixture.GenerateCustomerFixture();

        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Customer>().Any(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(false);

        // Action
        var action = () => _customerService.UpdateAsync(customerTest);

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>();
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Customer>().Any(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
    }

    [Fact]
    public async void Should_NotUpdateAsync_Throwing_ArgumentException_When_EmailAlreadyExist()
    {
        // Arrange
        var customerTest = CustomerFixture.GenerateCustomerFixture();

        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Customer>().Any(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(true);
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>()
        .AnyAsync(customer => customer.Email == customerTest.Email && customer.Id != customerTest.Id, default)).ReturnsAsync(true);
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().SingleResultQuery()
        .AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(It.IsAny<IQuery<Customer>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().SingleOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default)).ReturnsAsync(It.IsAny<Customer>());

        // Action
        var action = () => _customerService.UpdateAsync(customerTest);

        // Arrange
        await action.Should().ThrowAsync<ArgumentException>();
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Customer>().Any(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().AnyAsync(It.IsAny<Expression<Func<Customer, bool>>>(), default), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().SingleOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_NotUpdateAsync_Throwing_ArgumentException_When_CpfAlreadyExist()
    {
        // Arrange
        var customerTest = CustomerFixture.GenerateCustomerFixture();

        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Customer>().Any(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(true);
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>()
        .AnyAsync(customer => customer.Email == customerTest.Email && customer.Id != customerTest.Id, default)).ReturnsAsync(false);
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>()
        .AnyAsync(customer => customer.Cpf == customerTest.Cpf && customer.Id != customerTest.Id, default)).ReturnsAsync(true);
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().SingleResultQuery()
        .AndFilter(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(It.IsAny<IQuery<Customer>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Customer>().SingleOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default)).ReturnsAsync(It.IsAny<Customer>());

        // Action
        var action = () => _customerService.UpdateAsync(customerTest);

        // Arrange
        await action.Should().ThrowAsync<ArgumentException>();
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Customer>().Any(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().AnyAsync(It.IsAny<Expression<Func<Customer, bool>>>(), default), Times.Exactly(2));
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Customer>().SingleOrDefaultAsync(It.IsAny<IQuery<Customer>>(), default), Times.Once);
    }
}