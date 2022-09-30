using DomainModels.Models;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Services
{
    public class ProductService : IProductService
    {
        public long Create(Product productToCreate)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product productToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
