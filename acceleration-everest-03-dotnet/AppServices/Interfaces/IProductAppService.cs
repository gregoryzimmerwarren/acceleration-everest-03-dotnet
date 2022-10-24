using AppModels.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces;

public interface IProductAppService
{
    long Create(CreateProduct createProductDto);
    Task DeleteAsync(long productId);
    Task<IEnumerable<ProductResult>> GetAllProductsAsync();
    Task<ProductResult> GetProductByIdAsync(long productId);
    Task<decimal> GetProductUnitPriceByIdAsync(long productId);
    void Update(long productId, UpdateProduct updateProductDto);
}