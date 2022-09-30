using DomainModels.Models;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;

namespace DomainServices.Services
{
    public class PortifolioService : IPortifolioService
    {
        public long Create(Portfolio portfolioToCreate)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Portfolio> GetAllPortifolios()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Portfolio> GetPortifoliosByCustomerId(long customerId)
        {
            throw new NotImplementedException();
        }

        public Portfolio GetPortifoliosById(long id)
        {
            throw new NotImplementedException();
        }

        public void Invest(long customerId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public void Withdraw(long customerId, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
