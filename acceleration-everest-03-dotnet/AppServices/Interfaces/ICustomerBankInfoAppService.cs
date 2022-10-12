using AppModels.CustomersBankInfo;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface ICustomerBankInfoAppService
{
    void Create(long customerId);
    void Delete(long customerId);
    void Deposit(long customerId, decimal amount);
    IEnumerable<CustomerBankInfoResultDto> GetAllCustomersBankInfo();
    CustomerBankInfoResultDto GetCustomerBankInfoByCustomerId(long customerId);
    decimal GetTotalByCustomerId(long customerId);
    bool Withdraw(long customerId, decimal amount);
}