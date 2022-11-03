using DomainModels.Models;
using System.Threading.Tasks;

namespace DomainServices.Interfaces;

public interface IPortfolioProductService
{
    void Create(PortfolioProduct portfolioProductToCreate);
    Task DeleteAsync(long portfolioId, long productId);
    Task<PortfolioProduct> GetPortfolioProductByIdsAsync(long portfolioId, long productId);
}