using AppModels.CustomersBankInfo;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface ICustomerBankInfoAppService
{
    long Create(CreateCustomerBankInfoDto createCustomerBankInfoDto);
    void Deposit(long customerId, decimal amount);
    IEnumerable<CustomerBankInfoResultDto> GetAllCustomersBankInfo();
    decimal GetTotalById(long customerId);
    bool Withdraw(long customerId, decimal amount);
}