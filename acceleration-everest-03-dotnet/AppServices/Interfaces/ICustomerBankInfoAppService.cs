﻿using AppModels.CustomersBankInfo;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface ICustomerBankInfoAppService
{
    void Create(long customerId);
    void Deposit(long customerId, decimal amount);
    IEnumerable<CustomerBankInfoResultDto> GetAllCustomersBankInfo();
    decimal GetTotalById(long customerId);
    bool Withdraw(long customerId, decimal amount);
}