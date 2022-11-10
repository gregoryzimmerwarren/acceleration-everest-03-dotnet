using AppModels.Products;
using AppServices.Profiles;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;
using Infrastructure.CrossCutting.Enums;
using UnitTests.Fixtures.Products;

namespace UnitTests.Profiles;

public class ProductProfileTests : ProductProfile
{
    private readonly IMapper _mapper;

    public ProductProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ProductResult>();
            cfg.CreateMap<CreateProduct, Product>();
            cfg.CreateMap<UpdateProduct, Product>();
        });
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Should_MapTo_ProductResult_FromProduct_Successfully()
    {
        // Arrange
        var productTest = ProductFixture.GenerateProductFixture();

        var productResultTest = new ProductResult(
            id: productTest.Id,
            symbol: productTest.Symbol,
            unitPrice: productTest.UnitPrice,
            daysToExpire: productTest.DaysToExpire,
            expirationAt: productTest.ExpirationAt,
            type: productTest.Type);

        // Action
        var result = _mapper.Map<ProductResult>(productTest);

        // Assert
        result.Should().BeEquivalentTo(productResultTest);
    }

    [Fact]
    public void Should_MapTo_Product_FromCreateProduct_Successfully()
    {
        // Arrange
        var createProductTest = CreateProductFixture.GenerateCreateProductFixture();

        var productResultTest = new Product(
            symbol: createProductTest.Symbol,
            unitPrice: createProductTest.UnitPrice,
            issuanceAt: createProductTest.IssuanceAt,
            expirationAt: createProductTest.ExpirationAt,
            type: (ProductType)createProductTest.Type);

        // Action
        var result = _mapper.Map<Product>(createProductTest);

        // Assert
        result.Should().BeEquivalentTo(productResultTest);
    }

    [Fact]
    public void Should_MapTo_Product_FromUpdateProduct_Successfully()
    {
        // Arrange
        var updateProductTest = UpdateProductFixture.GenerateUpdateProductFixture();

        var productResultTest = new Product(
            symbol: updateProductTest.Symbol,
            unitPrice: updateProductTest.UnitPrice,
            issuanceAt: updateProductTest.IssuanceAt,
            expirationAt: updateProductTest.ExpirationAt,
            type: (ProductType)updateProductTest.Type);

        // Action
        var result = _mapper.Map<Product>(updateProductTest);

        // Assert
        result.Should().BeEquivalentTo(productResultTest);
    }
}