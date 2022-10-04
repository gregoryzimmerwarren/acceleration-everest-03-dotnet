using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Interfaces;

public interface IPortfolioProductServices
{
    long Create(PortfolioProduct portfolioProductToCreate);
    void Delete(long id);
    IEnumerable<PortfolioProduct> GetAllPortfolioProduct();
    PortfolioProduct GetPortfolioProductById(long id);
    IEnumerable<PortfolioProduct> GetPortfoliosByProductId(long productId);
    IEnumerable<PortfolioProduct> GetProductsByPortfolioId(long portfolioId);
}