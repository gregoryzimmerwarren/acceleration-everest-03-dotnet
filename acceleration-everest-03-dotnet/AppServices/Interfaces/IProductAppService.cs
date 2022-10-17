using AppModels.Products;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface IProductAppService
{
    long Create(CreateProduct createProductDto);
    void Delete(long productId);
    IEnumerable<ProductResult> GetAllProducts();
    ProductResult GetProductById(long productId);
    void Update(long productId, UpdateProduct updateProductDto);
}