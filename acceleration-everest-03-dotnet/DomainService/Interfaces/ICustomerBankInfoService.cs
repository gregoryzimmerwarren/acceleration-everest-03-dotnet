using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces;

public interface ICustomerBankInfoService
{
    void Create(long customerId);
    Task DeleteAsync(long customerId);
    Task DepositAsync(long customerId, decimal amount);
    Task<IEnumerable<CustomerBankInfo>> GetAllCustomersBankInfoAsync();
    Task<CustomerBankInfo> GetCustomerBankInfoByCustomerIdAsync(long customerId);
    Task<decimal> GetAccountBalanceByCustomerIdAsync(long customerId);
    Task<bool> WithdrawAsync(long customerId, decimal amount);
}