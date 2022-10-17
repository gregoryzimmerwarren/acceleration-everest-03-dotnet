using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces;

public interface ICustomerBankInfoService
{
    void Create(long customerId);
    void Delete(long customerId);
    void Deposit(long customerId, decimal amount);
    Task<IEnumerable<CustomerBankInfo>> GetAllCustomersBankInfoAsync();
    Task<CustomerBankInfo> GetCustomerBankInfoByCustomerIdAsync(long customerId);
    decimal GetTotalByCustomerId(long customerId);
    bool Withdraw(long customerId, decimal amount);
}