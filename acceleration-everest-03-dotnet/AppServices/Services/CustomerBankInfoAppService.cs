﻿using AppModels.CustomersBankInfo;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System.Collections.Generic;

namespace AppServices.Services;

public class CustomerBankInfoAppService : ICustomerBankInfoAppService
{
    private readonly ICustomerBankInfoService _customerBankInfoService;
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;

    public CustomerBankInfoAppService(
        ICustomerBankInfoService customerBankInfoService, 
        ICustomerService customerService, 
        IMapper mapper)
    {
        _customerBankInfoService = customerBankInfoService ?? throw new System.ArgumentNullException(nameof(customerBankInfoService));
        _customerService = customerService ?? throw new System.ArgumentNullException(nameof(customerService));
        _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    public void Create(long customerId)
    {
        _customerBankInfoService.Create(customerId);
    }

    public void Deposit(long customerId, decimal amount)
    {
        _customerBankInfoService.Deposit(customerId, amount);
    }

    public IEnumerable<CustomerBankInfoResultDto> GetAllCustomersBankInfo()
    {
        var customersBankInfo = _customerBankInfoService.GetAllCustomersBankInfo();

        foreach(CustomerBankInfo customerBankInfo in customersBankInfo)
        {
            var customer = _customerService.GetCustomerById(customerBankInfo.CustomerId);
            customerBankInfo.Customer = _mapper.Map<Customer>(customer);
        }            

        return _mapper.Map<IEnumerable<CustomerBankInfoResultDto>>(customersBankInfo);
    }

    public decimal GetTotalById(long customerId)
    {
        return _customerBankInfoService.GetTotalById(customerId);
    }

    public bool Withdraw(long customerId, decimal amount)
    {
        var result = _customerBankInfoService.Withdraw(customerId, amount);

        return result;
    }
}