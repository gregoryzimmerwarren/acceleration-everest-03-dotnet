using AppModels.CustomersBankInfo;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System.Collections.Generic;

namespace AppServices.Services;

public class CustomerBankInfoAppService : ICustomerBankInfoAppService
{
    private readonly ICustomerBankInfoService _customerBankInfoService;
    private readonly IMapper _mapper;

    public CustomerBankInfoAppService(ICustomerBankInfoService customerBankInfoService, IMapper mapper)
    {
        _customerBankInfoService = customerBankInfoService;
        _mapper = mapper;
    }

    public long Create(CreateCustomerBankInfoDto createCustomerBankInfoDto)
    {
        var customerBankInfoMapeado = _mapper.Map<CustomerBankInfo>(createCustomerBankInfoDto);

        return _customerBankInfoService.Create(customerBankInfoMapeado);
    }

    public void Deposit(long customerId, decimal amount)
    {
        _customerBankInfoService.Deposit(customerId, amount);
    }

    public IEnumerable<CustomerBankInfoResultDto> GetAllCustomersBankInfo()
    {
        var customersBankInfo = _customerBankInfoService.GetAllCustomersBankInfo();

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