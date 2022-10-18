using AppModels.PortfoliosProducts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces;

public interface IPortfolioProductAppService
{
    long Create(CreatePortfolioProduct createPortfolioProductDto);
    Task DeleteAsync(long portfolioId, long ProductId);
    Task<IEnumerable<PortfolioProductResult>> GetAllPortfolioProductAsync();
    Task<PortfolioProductResult> GetPortfolioProductByIdsAsync(long portfolioId, long ProductId);
    Task<IEnumerable<PortfolioProductResult>> GetPortfolioProductByProductIdAsync(long productId);
    Task<IEnumerable<PortfolioProductResult>> GetPortfolioProductByPortfolioIdAsync(long portfolioId);
}