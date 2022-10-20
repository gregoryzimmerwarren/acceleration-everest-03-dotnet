using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces;

public interface IPortfolioProductService
{
    void Create(PortfolioProduct portfolioProductToCreate);
    Task DeleteAsync(long portfolioId, long productId);
    Task<IEnumerable<PortfolioProduct>> GetAllPortfolioProductAsync();
    Task<PortfolioProduct> GetPortfolioProductByIdsAsync(long portfolioId, long productId);
    Task<IEnumerable<PortfolioProduct>> GetPortfolioProductByProductIdAsync(long productId);
    Task<IEnumerable<PortfolioProduct>> GetPortfolioProductByPortfolioIdAsync(long portfolioId);
}