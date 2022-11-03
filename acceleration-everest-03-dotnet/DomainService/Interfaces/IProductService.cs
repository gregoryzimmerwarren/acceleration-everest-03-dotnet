using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces;

public interface IProductService
{
    long Create(Product productToCreate);
    Task DeleteAsync(long id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(long id);
    Task<decimal> GetProductUnitPriceByIdAsync(long id);
    void Update(Product productToUpdate);
}