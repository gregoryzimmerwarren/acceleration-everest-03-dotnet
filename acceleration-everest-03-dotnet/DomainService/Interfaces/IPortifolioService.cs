using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Interfaces
{
    public interface IPortifolioService
    {
        long Create(Portfolio portfolioToCreate);
        void Delete(long id);
        IEnumerable<Portfolio> GetAllPortifolios();
        IEnumerable<Portfolio> GetPortifoliosByCustomerId(long customerId);
        Portfolio GetPortifoliosById(long id);
        void Invest(long customerId, decimal amount);
        void Withdraw(long customerId, decimal amount);
    }
}
