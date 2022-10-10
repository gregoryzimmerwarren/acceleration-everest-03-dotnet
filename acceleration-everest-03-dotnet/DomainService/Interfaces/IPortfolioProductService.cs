using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Interfaces;

public interface IPortfolioProductService
{
    long Create(PortfolioProduct portfolioProductToCreate);
    void Delete(long portfolioId, long productId);
    IEnumerable<PortfolioProduct> GetAllPortfolioProduct();
    PortfolioProduct GetPortfolioProductByIds(long portfolioId, long productId);
    IEnumerable<PortfolioProduct> GetPortfolioProductByProductId(long productId);
    IEnumerable<PortfolioProduct> GetPortfolioProductByPortfolioId(long portfolioId);
}