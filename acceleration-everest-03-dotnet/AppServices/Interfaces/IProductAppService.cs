using AppModels.Products;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface IProductAppService
{
    long Create(CreateProductDto createProductDto);
    void Delete(long productId);
    IEnumerable<ProductResultDto> GetAllProducts();
    ProductResultDto GetProductById(long productId);
    void Update(long productId, UpdateProductDto updateProductDto);
}