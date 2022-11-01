using DomainModels.Models;
using DomainServices.Interfaces;
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

public class PortfolioServiceTests
{
    private readonly Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>> _mockRepositoryFactory;
    private readonly Mock<IUnitOfWork<WarrenEverestDotnetDbContext>> _mockUnitOfWork;
    private readonly PortfolioService _portfolioService;

    public PortfolioServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork<WarrenEverestDotnetDbContext>>();
        _mockRepositoryFactory = new Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>>();
        _portfolioService = new PortfolioService(_mockRepositoryFactory.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public void Should_Create_Successfully()
    {
        // Arrange
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Portfolio>().Add(It.IsAny<Portfolio>()));

        // Action
        var result = _portfolioService.Create(portfolioTest);

        // Arrange
        result.Should().Be(1);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Portfolio>().Add(It.IsAny<Portfolio>()), Times.Once);
    }

    [Fact]
    public async void Should_DeleteAsync_Successfully()
    {
        // Arrange
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        portfolioTest.TotalBalance = 0;
        portfolioTest.AccountBalance = 0;
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolioTest);
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Portfolio>().Remove(portfolioTest));

        // Action
        await _portfolioService.DeleteAsync(portfolioTest.Id).ConfigureAwait(false);

        // Assert
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Portfolio>().Remove(It.IsAny<Portfolio>()), Times.Once);
    }

    [Fact]
    public async void Should_NotDeleteAsync_Throwing_ArgumentNullException_When_TotalBalance_IsBiggerThan0()
    {
        // Arrange
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        portfolioTest.AccountBalance = 0;
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolioTest);

        // Action
        var action = () => _portfolioService.DeleteAsync(portfolioTest.Id);

        // Assert
        await action.Should().ThrowAsync<ArgumentException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
    }
    
    [Fact]
    public async void Should_NotDeleteAsync_Throwing_ArgumentNullException_When_AcccountBalance_IsBiggerThan0()
    {
        // Arrange
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        portfolioTest.TotalBalance = 0;
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolioTest);

        // Action
        var action = () => _portfolioService.DeleteAsync(portfolioTest.Id);

        // Assert
        await action.Should().ThrowAsync<ArgumentException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_DepositAsync_Successfully()
    {
        // Arrange
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        decimal amountTest = 17.05m;
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolioTest);
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Portfolio>().Update(portfolioTest));

        // Action
        await _portfolioService.DepositAsync(portfolioTest.Id, amountTest).ConfigureAwait(false);

        // Assert
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Portfolio>().Update(It.IsAny<Portfolio>()), Times.Once);
    }

    [Fact]
    public async void Should_GetPortfolioByIdAsync_Successfully()
    {
        // Arrange
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolioTest);

        // Action
        await _portfolioService.GetPortfolioByIdAsync(portfolioTest.Id).ConfigureAwait(false);

        // Assert
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_NotGetPortfolioByIdAsync_Throwing_ArgumentNullException()
    {
        // Arrange
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().
        SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(It.IsAny<Portfolio>());

        // Action
        var action = () => _portfolioService.GetPortfolioByIdAsync(portfolioTest.Id);

        // Assert
        await action.Should().ThrowAsync<ArgumentNullException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_GetPortfoliosByCustomerIdAsync_Successfully()
    {
        // Arrange
        long customerIdTest = 1;
        var listportfoliosTest = PortfolioFixture.GenerateListPortfolioFixture(3);
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SearchAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(listportfoliosTest);

        // Action
        await _portfolioService.GetPortfoliosByCustomerIdAsync(customerIdTest).ConfigureAwait(false);

        // Assert
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SearchAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_NotGetPortfoliosByCustomerIdAsync_Throwing_ArgumentNullException()
    {
        // Arrange
        long customerIdTest = 1;
        var listportfoliosTest = PortfolioFixture.GenerateListPortfolioFixture(0);
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>()
        .SearchAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(listportfoliosTest);

        // Action
        var action = () => _portfolioService.GetPortfoliosByCustomerIdAsync(customerIdTest);

        // Assert
        await action.Should().ThrowAsync<ArgumentNullException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().MultipleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SearchAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_InvestAsync_Successfully()
    {
        // Arrange
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        portfolioTest.AccountBalance = 20m;
        decimal amountTest = 17.05m;
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolioTest);
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Portfolio>().Update(portfolioTest));

        // Action
        await _portfolioService.InvestAsync(portfolioTest.Id, amountTest).ConfigureAwait(false);

        // Assert
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Portfolio>().Update(It.IsAny<Portfolio>()), Times.Once);
    }

    [Fact]
    public async void Should_NotInvestAsync_ArgumentNullException_When_Amount_IsBiggerThan_AcccountBalance()
    {
        // Arrange
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        portfolioTest.AccountBalance = 17.05m;
        decimal amountTest = 20m;
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolioTest);

        // Action
        var action = () => _portfolioService.InvestAsync(portfolioTest.Id, amountTest);

        // Assert
        await action.Should().ThrowAsync<ArgumentException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_RedeemToPortfolioAsync_Successfully()
    {
        // Arrange
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        portfolioTest.TotalBalance = 20m;
        decimal amountTest = 17.05m;
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolioTest);
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Portfolio>().Update(portfolioTest));

        // Action
        await _portfolioService.RedeemToPortfolioAsync(portfolioTest.Id, amountTest).ConfigureAwait(false);

        // Assert
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Portfolio>().Update(It.IsAny<Portfolio>()), Times.Once);
    }

    [Fact]
    public async void Should_NotRedeemToPortfolioAsync_ArgumentNullException_When_Amount_IsBiggerThan_TotalBalance()
    {
        // Arrange
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        portfolioTest.TotalBalance = 17.05m;
        decimal amountTest = 20m;
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolioTest);

        // Action
        var action = () => _portfolioService.RedeemToPortfolioAsync(portfolioTest.Id, amountTest);

        // Assert
        await action.Should().ThrowAsync<ArgumentException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_WithdrawFromPortfolioAsync_Successfully()
    {
        // Arrange
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        portfolioTest.AccountBalance = 20m;
        decimal amountTest = 17.05m;
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolioTest);
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Portfolio>().Update(portfolioTest));

        // Action
        await _portfolioService.WithdrawFromPortfolioAsync(portfolioTest.Id, amountTest).ConfigureAwait(false);

        // Assert
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Portfolio>().Update(It.IsAny<Portfolio>()), Times.Once);
    }

    [Fact]
    public async void Should_NotWithdrawFromPortfolioAsync_ArgumentNullException_When_Amount_IsBiggerThan_AccountBalance()
    {
        // Arrange
        var portfolioTest = PortfolioFixture.GeneratePortfolioFixture();
        portfolioTest.AccountBalance = 17.05m;
        decimal amountTest = 20m;
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>())).Returns(It.IsAny<IQuery<Portfolio>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default)).ReturnsAsync(portfolioTest);

        // Action
        var action = () => _portfolioService.WithdrawFromPortfolioAsync(portfolioTest.Id, amountTest);

        // Assert
        await action.Should().ThrowAsync<ArgumentException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Portfolio, bool>>>())
        .Include(It.IsAny<Func<IQueryable<Portfolio>, IIncludableQueryable<Portfolio, object>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Portfolio>().SingleOrDefaultAsync(It.IsAny<IQuery<Portfolio>>(), default), Times.Once);
    }
}