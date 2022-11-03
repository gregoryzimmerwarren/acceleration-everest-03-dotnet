using DomainModels.Models;
using DomainServices.Services;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Infrastructure.Data;
using Moq;
using System.Linq.Expressions;
using UnitTests.Fixtures.Products;

namespace UnitTests.DomainServices;

public class ProductServiceTests
{
    private readonly Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>> _mockRepositoryFactory;
    private readonly Mock<IUnitOfWork<WarrenEverestDotnetDbContext>> _mockUnitOfWork;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork<WarrenEverestDotnetDbContext>>();
        _mockRepositoryFactory = new Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>>();
        _productService = new ProductService(_mockUnitOfWork.Object, _mockRepositoryFactory.Object);
    }

    [Fact]
    public void Should_Create_Successfully()
    {
        // Arrange
        var productTest = ProductFixture.GenerateProductFixture();

        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Product>().Add(It.IsAny<Product>()));

        // Action
        var result = _productService.Create(productTest);

        // Arrange
        result.Should().Be(1);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Product>().Add(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async void Should_DeleteAsync_Successfully()
    {
        // Arrange
        var productTest = ProductFixture.GenerateProductFixture();

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Product>().SingleResultQuery()
        .AndFilter(It.IsAny<Expression<Func<Product, bool>>>())).Returns(It.IsAny<IQuery<Product>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Product>().SingleOrDefaultAsync(It.IsAny<IQuery<Product>>(), default)).ReturnsAsync(productTest);
        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Product>().Remove(productTest));

        // Action
        await _productService.DeleteAsync(productTest.Id).ConfigureAwait(false);

        // Assert
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Product>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Product>().SingleOrDefaultAsync(It.IsAny<IQuery<Product>>(), default), Times.Once);
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Product>().Remove(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async void Should_GetAllProductsAsync_Successfully()
    {
        // Arrange
        var listProductTest = ProductFixture.GenerateListProductFixture(3);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Product>().MultipleResultQuery()).Returns(It.IsAny<IMultipleResultQuery<Product>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Product>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Product>>(), default)).ReturnsAsync(listProductTest);

        // Action
        var result = await _productService.GetAllProductsAsync().ConfigureAwait(false);

        // Arrange
        result.Should().NotBeNull();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Product>().MultipleResultQuery(), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Product>().SearchAsync(It.IsAny<IMultipleResultQuery<Product>>(), default), Times.Once);
    }

    [Fact]
    public async void ShouldNot_GetAllProductsAsync_Throwing_ArgumentException()
    {
        // Arrange
        var listProductTest = ProductFixture.GenerateListProductFixture(0);

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Product>().MultipleResultQuery()).Returns(It.IsAny<IMultipleResultQuery<Product>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Product>()
        .SearchAsync(It.IsAny<IMultipleResultQuery<Product>>(), default)).ReturnsAsync(listProductTest);

        // Action
        var action = () => _productService.GetAllProductsAsync();

        // Arrange
        await action.Should().ThrowAsync<ArgumentException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Product>().MultipleResultQuery(), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Product>().SearchAsync(It.IsAny<IMultipleResultQuery<Product>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_GetProductByIdAsync_Successfully()
    {
        // Arrange
        var productTest = ProductFixture.GenerateProductFixture();

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Product>().SingleResultQuery()
        .AndFilter(It.IsAny<Expression<Func<Product, bool>>>())).Returns(It.IsAny<IQuery<Product>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Product>().SingleOrDefaultAsync(It.IsAny<IQuery<Product>>(), default)).ReturnsAsync(productTest);

        // Action
        var result = await _productService.GetProductByIdAsync(productTest.Id).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Product>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Product>().SingleOrDefaultAsync(It.IsAny<IQuery<Product>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_GetProductByIdAsync_Throwing_ArgumentNullException()
    {
        // Arrange
        var productTest = ProductFixture.GenerateProductFixture();

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Product>().SingleResultQuery()
        .AndFilter(It.IsAny<Expression<Func<Product, bool>>>())).Returns(It.IsAny<IQuery<Product>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Product>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<Product>>(), default)).ReturnsAsync(It.IsAny<Product>());

        // Action
        var action = () => _productService.GetProductByIdAsync(productTest.Id);

        // Assert
        await action.Should().ThrowAsync<ArgumentNullException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Product>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Product>().SingleOrDefaultAsync(It.IsAny<IQuery<Product>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_GetProductUnitPriceByIdAsync_Successfully()
    {
        // Arrange
        var productTest = ProductFixture.GenerateProductFixture();

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Product>().SingleResultQuery()
        .AndFilter(It.IsAny<Expression<Func<Product, bool>>>())).Returns(It.IsAny<IQuery<Product>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Product>().SingleOrDefaultAsync(It.IsAny<IQuery<Product>>(), default)).ReturnsAsync(productTest);

        // Action
        var result = await _productService.GetProductUnitPriceByIdAsync(productTest.Id).ConfigureAwait(false);

        // Assert
        result.Should().BeGreaterThanOrEqualTo(0);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Product>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Product>().SingleOrDefaultAsync(It.IsAny<IQuery<Product>>(), default), Times.Once);
    }

    [Fact]
    public async void Should_GetProductUnitPriceByIdAsync_Throwing_ArgumentNullException()
    {
        // Arrange
        var productTest = ProductFixture.GenerateProductFixture();

        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Product>().SingleResultQuery()
        .AndFilter(It.IsAny<Expression<Func<Product, bool>>>())).Returns(It.IsAny<IQuery<Product>>());
        _mockRepositoryFactory.Setup(repositoryFactory => repositoryFactory.Repository<Product>()
        .SingleOrDefaultAsync(It.IsAny<IQuery<Product>>(), default)).ReturnsAsync(It.IsAny<Product>());

        // Action
        var action = () => _productService.GetProductUnitPriceByIdAsync(productTest.Id);

        // Assert
        await action.Should().ThrowAsync<ArgumentNullException>();
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Product>().SingleResultQuery().AndFilter(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
        _mockRepositoryFactory.Verify(repositoryFactory => repositoryFactory.Repository<Product>().SingleOrDefaultAsync(It.IsAny<IQuery<Product>>(), default), Times.Once);
    }

    [Fact]
    public void Should_Update_Successfully()
    {
        // Arrange
        var productTest = ProductFixture.GenerateProductFixture();

        _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Repository<Product>().Update(It.IsAny<Product>()));

        // Action
        _productService.Update(productTest);

        // Arrange        
        _mockUnitOfWork.Verify(unitOfWork => unitOfWork.Repository<Product>().Update(It.IsAny<Product>()), Times.Once);
    }
}