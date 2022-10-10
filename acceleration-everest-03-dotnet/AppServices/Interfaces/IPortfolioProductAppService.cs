using AppModels.PortfoliosProducts;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface IPortfolioProductAppService
{
    long Create(CreatePortfolioProductDto createPortfolioProductDto);
    void Delete(long portfolioId, long ProductId);
    IEnumerable<PortfolioProductResultDto> GetAllPortfolioProduct();
    PortfolioProductResultDto GetPortfolioProductByIds(long portfolioId, long ProductId);
    IEnumerable<PortfolioProductResultDto> GetPortfolioProductByProductId(long productId);
    IEnumerable<PortfolioProductResultDto> GetPortfolioProductByPortfolioId(long portfolioId);
}