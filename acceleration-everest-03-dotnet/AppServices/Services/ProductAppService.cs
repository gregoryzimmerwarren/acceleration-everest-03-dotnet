using AppModels.Products;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System.Collections.Generic;

namespace AppServices.Services;

public class ProductAppService : IProductAppService
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductAppService(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    public long Create(CreateProductDto createProductDto)
    {
        var productMapeado = _mapper.Map<Product>(createProductDto);

        return _productService.Create(productMapeado);
    }

    public void Delete(long productId)
    {
        _productService.Delete(productId);
    }

    public IEnumerable<ProductResultDto> GetAllProducts()
    {
        var products = _productService.GetAllProducts();

        return _mapper.Map<IEnumerable<ProductResultDto>>(products);
    }

    public ProductResultDto GetProductById(long productId)
    {
        var product = _productService.GetProductById(productId);

        return _mapper.Map<ProductResultDto>(product);
    }

    public void Update(long productId, UpdateProductDto updateProductDto)
    {
        var productMapeado = _mapper.Map<Product>(updateProductDto);
        productMapeado.Id = productId;

        _productService.Update(productMapeado);
    }
}