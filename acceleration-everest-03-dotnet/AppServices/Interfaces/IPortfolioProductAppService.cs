using AppModels.PortfoliosProducts;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface IPortfolioProductAppService
{
    long Create(CreatePortfolioProductDto createPortfolioProductDto);
    void Delete(long portfolioProductId);
    IEnumerable<PortfolioProductResultDto> GetAllPortfolioProduct();
    PortfolioProductResultDto GetPortfolioProductById(long portfolioProductId);
    IEnumerable<PortfolioProductResultDto> GetPortfolioProductByProductId(long productId);
    IEnumerable<PortfolioProductResultDto> GetPortfolioProductByPortfolioId(long portfolioId);
}