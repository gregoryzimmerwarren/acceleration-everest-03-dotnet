using AppModels.CustomersBankInfo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces;

public interface ICustomerBankInfoAppService
{
    void Create(long customerId);
    void Delete(long customerId);
    void Deposit(long customerId, decimal amount);
    Task<IEnumerable<CustomerBankInfoResult>> GetAllCustomersBankInfoAsync();
    Task<CustomerBankInfoResult> GetCustomerBankInfoByCustomerIdAsync(long customerId);
    decimal GetTotalByCustomerId(long customerId);
    bool Withdraw(long customerId, decimal amount);
}