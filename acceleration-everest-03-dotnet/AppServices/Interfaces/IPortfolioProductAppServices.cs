using AppModels.PortfoliosProducts;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface IPortfolioProductAppServices
{
    long Create(CreatePortfolioProductDto createPortfolioProductDto);
    void Delete(long portfolioProductId);
    IEnumerable<PortfolioProductResultDto> GetAllPortfolioProduct();
    PortfolioProductResultDto GetPortfolioProductById(long portfolioProductId);
    IEnumerable<PortfolioProductResultDto> GetPortfoliosByProductId(long productId);
    IEnumerable<PortfolioProductResultDto> GetProductsByPortfolioId(long portfolioId);
}