using DomainModels.Models;
using DomainServices.Services;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;
using UnitTests.Fixtures.PortfoliosProducts;

namespace UnitTests.DomainServices;

public class PortfolioProductServiceTests
{
    private readonly Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>> _mockRepositoryFactory;
    private readonly Mock<IUnitOfWork<WarrenEverestDotnetDbContext>> _mockUnitOfWork;
    private readonly PortfolioProductService _portfolioProductService;

    public PortfolioProductServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork<WarrenEverestDotnetDbContext>>();
        _mockRepositoryFactory = new Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>>();
        _portfolioProductService = new PortfolioProductService(_mockUnitOfWork.Object, _mockRepositoryFactory.Object);
    }

    [Fact]
    public void Should_Create_Successfully()
    {
        // Arrange
        var portfolioProductTest = PortfolioProductFixture.GeneratePortfolioProductFixture();

        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<PortfolioProduct>().Add(It.IsAny<PortfolioProduct>()));

        // Action
        _portfolioProductService.Create(portfolioProductTest);

        // Arrange
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<PortfolioProduct>().Add(It.IsAny<PortfolioProduct>()), Times.Once);
    }

    [Fact]
    public async void Should_DeleteAsync_Successfully()
    {
        // Arrange        
        var portfolioProductBankInfoTest = PortfolioProductFixture.GeneratePortfolioProductFixture();

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<PortfolioProduct>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<PortfolioProduct, bool>>>())
        .Include(It.IsAny<Func<IQueryable<PortfolioProduct>, IIncludableQueryable<PortfolioProduct, object>>>()))
            .Returns(It.IsAny<IQuery<PortfolioProduct>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<PortfolioProduct>()
        .FirstOrDefaultAsync(It.IsAny<IQuery<PortfolioProduct>>(), default))
            .ReturnsAsync(portfolioProductBankInfoTest);

        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<PortfolioProduct>().Remove(It.IsAny<PortfolioProduct>()));

        // Action
        await _portfolioProductService.DeleteAsync(It.IsAny<long>(), It.IsAny<long>()).ConfigureAwait(false);

        // Arrange
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<PortfolioProduct>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<PortfolioProduct, bool>>>())
        .Include(It.IsAny<Func<IQueryable<PortfolioProduct>, IIncludableQueryable<PortfolioProduct, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<PortfolioProduct>()
        .FirstOrDefaultAsync(It.IsAny<IQuery<PortfolioProduct>>(), default), Times.Once);

        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<PortfolioProduct>().Remove(It.IsAny<PortfolioProduct>()), Times.Once);
    }

    [Fact]
    public async void Should_GetPortfolioProductByIdsAsync_Successfully()
    {
        // Arrange        
        var portfolioProductBankInfoTest = PortfolioProductFixture.GeneratePortfolioProductFixture();

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<PortfolioProduct>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<PortfolioProduct, bool>>>())
        .Include(It.IsAny<Func<IQueryable<PortfolioProduct>, IIncludableQueryable<PortfolioProduct, object>>>()))
            .Returns(It.IsAny<IQuery<PortfolioProduct>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<PortfolioProduct>()
        .FirstOrDefaultAsync(It.IsAny<IQuery<PortfolioProduct>>(), default))
            .ReturnsAsync(portfolioProductBankInfoTest);

        // Action
        var result = await _portfolioProductService.GetPortfolioProductByIdsAsync(It.IsAny<long>(), It.IsAny<long>()).ConfigureAwait(false);

        // Arrange
        result.Should().NotBeNull();

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<PortfolioProduct>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<PortfolioProduct, bool>>>())
        .Include(It.IsAny<Func<IQueryable<PortfolioProduct>, IIncludableQueryable<PortfolioProduct, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<PortfolioProduct>()
        .FirstOrDefaultAsync(It.IsAny<IQuery<PortfolioProduct>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_NotGetPortfolioProductByIdsAsync__Throwing_ArgumentNullException()
    {
        // Arrange        
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<PortfolioProduct>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<PortfolioProduct, bool>>>())
        .Include(It.IsAny<Func<IQueryable<PortfolioProduct>, IIncludableQueryable<PortfolioProduct, object>>>()))
            .Returns(It.IsAny<IQuery<PortfolioProduct>>());

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<PortfolioProduct>()
        .FirstOrDefaultAsync(It.IsAny<IQuery<PortfolioProduct>>(), default))
            .ReturnsAsync(It.IsAny<PortfolioProduct>());

        // Action
        var action = () => _portfolioProductService.GetPortfolioProductByIdsAsync(It.IsAny<long>(), It.IsAny<long>());

        // Arrange
        await action.Should().ThrowAsync<ArgumentNullException>();

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<PortfolioProduct>()
        .SingleResultQuery().AndFilter(It.IsAny<Expression<Func<PortfolioProduct, bool>>>())
        .Include(It.IsAny<Func<IQueryable<PortfolioProduct>, IIncludableQueryable<PortfolioProduct, object>>>()), Times.Once);

        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<PortfolioProduct>()
        .FirstOrDefaultAsync(It.IsAny<IQuery<PortfolioProduct>>(), default), Times.Once);
    }
}