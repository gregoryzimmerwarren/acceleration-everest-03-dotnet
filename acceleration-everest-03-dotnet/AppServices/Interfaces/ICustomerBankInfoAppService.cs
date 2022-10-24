using AppModels.CustomersBankInfo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces;

public interface ICustomerBankInfoAppService
{
    void Create(long customerId);
    Task DeleteAsync(long customerId);
    Task DepositAsync(long customerId, decimal amount);
    Task<IEnumerable<CustomerBankInfoResult>> GetAllCustomersBankInfoAsync();
    Task<decimal> GetTotalByCustomerIdAsync(long customerId);
    Task<bool> WithdrawAsync(long customerId, decimal amount);
}