using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryFactory _repositoryFactory;

        public ProductService(
            IUnitOfWork unitOfWork,
            IRepositoryFactory repositoryFactory)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositoryFactory = repositoryFactory ?? (IRepositoryFactory)_unitOfWork;
        }

        public long Create(Product productToCreate)
        {
            var repository = _unitOfWork.Repository<Product>();
            repository.Add(productToCreate);
            _unitOfWork.SaveChanges();

            return productToCreate.Id;
        }

        public void Delete(long id)
        {
            var product = GetProductById(id);
            var repository = _unitOfWork.Repository<Product>();
            repository.Remove(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var repository = _repositoryFactory.Repository<Product>();
            var query = repository.MultipleResultQuery();
            var products = repository.Search(query);

            if (products == null)
                throw new ArgumentException($"No product found");

            return products;
        }

        public Product GetProductById(long id)
        {
            var repository = _repositoryFactory.Repository<Product>();
            var query = repository.SingleResultQuery().AndFilter(product => product.Id == id);
            var product = repository.SingleOrDefault(query);

            if (product == null)
                throw new ArgumentException($"No product found for Id: {id}");

            return product;
        }

        public void Update(Product productToUpdate)
        {
            var repository = _unitOfWork.Repository<Product>();
            repository.Update(productToUpdate);
            _unitOfWork.SaveChanges();
        }
    }
}
