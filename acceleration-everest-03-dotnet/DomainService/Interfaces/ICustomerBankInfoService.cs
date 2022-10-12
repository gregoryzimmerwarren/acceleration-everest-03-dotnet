using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Interfaces;

public interface ICustomerBankInfoService
{
    void Create(long customerId);
    void Delete(long customerId);
    void Deposit(long customerId, decimal amount);
    IEnumerable<CustomerBankInfo> GetAllCustomersBankInfo();
    CustomerBankInfo GetCustomerBankInfoByCustomerId(long customerId);
    decimal GetTotalByCustomerId(long customerId);
    bool Withdraw(long customerId, decimal amount);
}