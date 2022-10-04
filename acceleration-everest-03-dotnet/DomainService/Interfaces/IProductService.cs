using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Interfaces;

public interface IProductService
{
    long Create(Product productToCreate);
    void Delete(long id);
    IEnumerable<Product> GetAllProducts();
    Product GetProductById(long id);
    void Update(Product productToUpdate);
}