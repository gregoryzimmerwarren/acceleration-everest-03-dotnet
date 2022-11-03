using AppModels.Products;
using AppServices.Services;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using DomainServices.Services;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using UnitTests.Fixtures.Products;

namespace UnitTests.AppServices;

public class ProductAppServiceTests
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly ProductAppService _productAppService;
    private readonly IMapper _mapper;

    public ProductAppServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ProductResult>();
            cfg.CreateMap<CreateProduct, Product>();
            cfg.CreateMap<UpdateProduct, Product>();
        });
        _mapper = config.CreateMapper();
        _mockProductService = new Mock<IProductService>();
        _productAppService = new ProductAppService(_mockProductService.Object, _mapper);
    }

    [Fact]
    public void Should_CreateProduct_Successfully()
    {
        // Arrange  
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();
        var productTest = ProductFixture.GenerateProductFixture();

        _mockProductService.Setup(productService => productService.Create(It.IsAny<Product>())).Returns(It.IsAny<long>());

        // Action
        var result = _productAppService.Create(createProductTest);

        // Assert
        result.Should().NotBe(null);

        _mockProductService.Verify(productService => productService.Create(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async void Should_DeleteAsync_Successfully()
    {
        // Arrange  
        long productIdTest = 1;

        _mockProductService.Setup(productService => productService.DeleteAsync(It.IsAny<long>()));

        // Action
        await _productAppService.DeleteAsync(productIdTest).ConfigureAwait(false);

        // Assert
        _mockProductService.Verify(productService => productService.DeleteAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_GetAllProductsAsync_Successfully()
    {
        // Arrange        
        var listProductsResultTest = ProductResultFixture.GenerateListProductResultFixture(3);
        var listProductTest = ProductFixture.GenerateListProductFixture(3);

        _mockProductService.Setup(productService => productService.GetAllProductsAsync()).ReturnsAsync(listProductTest);
        _mapper.Map<IEnumerable<ProductResult>>(listProductTest);

        // Action
        var result = await _productAppService.GetAllProductsAsync().ConfigureAwait(false);

        // Assert
        result.Should().HaveCountGreaterThanOrEqualTo(3);
        _mockProductService.Verify(productService => productService.GetAllProductsAsync(), Times.Once);
    }

    [Fact]
    public async void Should_GetProductByIdAsync_Successfully()
    {
        // Arrange
        long idTest = 1;
        var productTest = ProductFixture.GenerateProductFixture();
        productTest.Id = idTest;

        _mockProductService.Setup(productService => productService.GetProductByIdAsync(It.IsAny<long>())).ReturnsAsync(productTest);
        _mapper.Map<ProductResult>(productTest);

        // Action
        var result = await _productAppService.GetProductByIdAsync(productTest.Id).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        _mockProductService.Verify(productService => productService.GetProductByIdAsync(idTest), Times.Once);
    }

    [Fact]
    public void Should_GetProductUnitPriceByIdAsync_Successfully()
    {
        // Arrange
        long productIdTest = 1;

        _mockProductService.Setup(productService => productService.GetProductUnitPriceByIdAsync(It.IsAny<long>())).ReturnsAsync(It.IsAny<decimal>());

        // Action
        var result = _productAppService.GetProductUnitPriceByIdAsync(productIdTest).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        _mockProductService.Verify(productService => productService.GetProductUnitPriceByIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public void Should_UpdateProduct_Successfully()
    {
        // Arrange
        var updateProductTest = UpdateProductFixture.GenerateUpdateProductFixture();
        var productTest = ProductFixture.GenerateProductFixture();
        long productIdTest = 1;

        _mockProductService.Setup(productService => productService.Update(It.IsAny<Product>()));

        // Action
        _productAppService.Update(productIdTest, updateProductTest);

        // Assert
        _mockProductService.Verify(productService => productService.Update(It.IsAny<Product>()), Times.Once);
    }
}
