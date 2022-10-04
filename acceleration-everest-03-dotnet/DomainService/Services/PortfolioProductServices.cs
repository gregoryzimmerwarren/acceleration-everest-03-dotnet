using DomainModels.Models;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using System.Collections.Generic;
using System;
using DomainServices.Interfaces;

namespace DomainServices.Services
{
    public class PortfolioProductServices : IPortfolioProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryFactory _repositoryFactory;

        public PortfolioProductServices(
            IUnitOfWork unitOfWork,
            IRepositoryFactory repositoryFactory)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositoryFactory = repositoryFactory ?? (IRepositoryFactory)_unitOfWork;
        }

        public long Create(PortfolioProduct portfolioProductToCreate)
        {
            var repository = _unitOfWork.Repository<PortfolioProduct>();
            repository.Add(portfolioProductToCreate);
            _unitOfWork.SaveChanges();

            return portfolioProductToCreate.Id;
        }

        public void Delete(long id)
        {
            var portfolioProduct = GetPortfolioProductById(id);
            var repository = _unitOfWork.Repository<PortfolioProduct>();
            repository.Remove(portfolioProduct);
        }

        public IEnumerable<PortfolioProduct> GetAllPortfolioProduct()
        {
            var repository = _repositoryFactory.Repository<PortfolioProduct>();
            var query = repository.MultipleResultQuery();
            var portfoliosProducts = repository.Search(query);

            if (portfoliosProducts == null)
                throw new ArgumentException($"No relationship between portfolio and product found");

            return portfoliosProducts;
        }

        public PortfolioProduct GetPortfolioProductById(long id)
        {
            var repository = _repositoryFactory.Repository<PortfolioProduct>();
            var query = repository.SingleResultQuery().AndFilter(portfolioProduct => portfolioProduct.Id == id);
            var portfolioProduct = repository.SingleOrDefault(query);

            if (portfolioProduct == null)
                throw new ArgumentException($"No relationship between portfolio and product found for Id: {id}");

            return portfolioProduct;
        }

        public IEnumerable<PortfolioProduct> GetPortfoliosByProductId(long productId)
        {
            var repository = _repositoryFactory.Repository<PortfolioProduct>();
            var query = repository.MultipleResultQuery().AndFilter(portfolioProduct => portfolioProduct.ProductId == productId);
            var portfoliosProducts = repository.Search(query);

            if (portfoliosProducts.Count == 0)
                throw new ArgumentException($"No portfolio found for product Id: {productId}");

            return portfoliosProducts;
        }

        public IEnumerable<PortfolioProduct> GetProductsByPortfolioId(long portfolioId)
        {
            var repository = _repositoryFactory.Repository<PortfolioProduct>();
            var query = repository.MultipleResultQuery().AndFilter(portfolioProduct => portfolioProduct.PortfolioId == portfolioId);
            var portfoliosProducts = repository.Search(query);

            if (portfoliosProducts.Count == 0)
                throw new ArgumentException($"No product found for portfolio Id: {portfolioId}");

            return portfoliosProducts;
        }
    }
}
