using AppModels.PortfoliosProducts;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface IPortfolioProductAppService
{
    long Create(CreatePortfolioProduct createPortfolioProductDto);
    void Delete(long portfolioId, long ProductId);
    IEnumerable<PortfolioProductResult> GetAllPortfolioProduct();
    PortfolioProductResult GetPortfolioProductByIds(long portfolioId, long ProductId);
    IEnumerable<PortfolioProductResult> GetPortfolioProductByProductId(long productId);
    IEnumerable<PortfolioProductResult> GetPortfolioProductByPortfolioId(long portfolioId);
}