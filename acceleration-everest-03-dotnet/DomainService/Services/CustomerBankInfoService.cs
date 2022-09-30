using DomainModels.Models;
using DomainServices.Interfaces;
using System.Collections.Generic;

namespace DomainServices.Services
{
    public class CustomerBankInfoService : ICustomerBankInfoService
    {
        public long Create(CustomerBankInfo customerBankInfoToCreate)
        {
            throw new System.NotImplementedException();
        }

        public void Deposit(long customerId, decimal amount)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CustomerBankInfo> GetAllCustomersBankInfo()
        {
            throw new System.NotImplementedException();
        }

        public decimal GetTotalById(long customerId)
        {
            throw new System.NotImplementedException();
        }

        public void Withdraw(long customerId, decimal amount)
        {
            throw new System.NotImplementedException();
        }
    }
}
