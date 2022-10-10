using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Interfaces;

public interface IPortfolioProductService
{
    long Create(PortfolioProduct portfolioProductToCreate);
    void Delete(long id);
    IEnumerable<PortfolioProduct> GetAllPortfolioProduct();
    PortfolioProduct GetPortfolioProductById(long id);
    IEnumerable<PortfolioProduct> GetPortfolioProductByProductId(long productId);
    IEnumerable<PortfolioProduct> GetPortfolioProductByPortfolioId(long portfolioId);
}