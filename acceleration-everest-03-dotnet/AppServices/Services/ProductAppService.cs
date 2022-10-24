using AppModels.Products;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Services;

public class ProductAppService : IProductAppService
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductAppService(
        IProductService productService,
        IMapper mapper)
    {
        _productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    public long Create(CreateProduct createProductDto)
    {
        var mappedProduct = _mapper.Map<Product>(createProductDto);
        mappedProduct.DaysToExpire = (mappedProduct.ExpirationAt.Subtract(mappedProduct.IssuanceAt)).Days;

        return _productService.Create(mappedProduct);
    }

    public async Task DeleteAsync(long productId)
    {
        await _productService.DeleteAsync(productId).ConfigureAwait(false);
    }

    public async Task<IEnumerable<ProductResult>> GetAllProductsAsync()
    {
        var products = await _productService.GetAllProductsAsync().ConfigureAwait(false);

        return _mapper.Map<IEnumerable<ProductResult>>(products);
    }

    public async Task<ProductResult> GetProductByIdAsync(long productId)
    {
        var product = await _productService.GetProductByIdAsync(productId).ConfigureAwait(false);

        return _mapper.Map<ProductResult>(product);
    }

    public async Task<decimal> GetProductUnitPriceByIdAsync(long productId)
    {
        return await _productService.GetProductUnitPriceByIdAsync(productId).ConfigureAwait(false);
    }

    public void Update(long productId, UpdateProduct updateProductDto)
    {
        var mappedProduct = _mapper.Map<Product>(updateProductDto);
        mappedProduct.Id = productId;

        _productService.Update(mappedProduct);
    }
}