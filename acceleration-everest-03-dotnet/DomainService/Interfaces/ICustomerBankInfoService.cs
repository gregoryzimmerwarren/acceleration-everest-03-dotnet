using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Interfaces;

public interface ICustomerBankInfoService
{
    long Create(CustomerBankInfo customerBankInfoToCreate);
    void Deposit(long customerId, decimal amount);
    IEnumerable<CustomerBankInfo> GetAllCustomersBankInfo();
    CustomerBankInfo GetCustomerBankInfoByCustomerId(long customerId);
    decimal GetTotalById(long customerId);
    bool Withdraw(long customerId, decimal amount);
}