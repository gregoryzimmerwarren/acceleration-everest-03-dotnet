using AppModels.Customers;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;

namespace AppServices.Services;
public class CustomerAppService : ICustomerAppService
{
    private readonly ICustomerBankInfoService _customerBankInfoService;
    private readonly IPortfolioService _portfolioService;
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;

    public CustomerAppService(
        ICustomerBankInfoService customerBankInfoService,
        IPortfolioService portfolioService,
        ICustomerService customerService,
        IMapper mapper)
    {
        _customerBankInfoService = customerBankInfoService ?? throw new ArgumentNullException(nameof(customerBankInfoService));
        _portfolioService = portfolioService ?? throw new ArgumentNullException(nameof(portfolioService));
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public long Create(CreateCustomerDto createCustomerDto)
    {
        var customerMapeado = _mapper.Map<Customer>(createCustomerDto);
        var newCustomerId = _customerService.Create(customerMapeado);

        _customerBankInfoService.Create(newCustomerId);

        return newCustomerId;
    }

    public void Delete(long customerId)
    {
        _customerService.Delete(customerId);
        _customerBankInfoService.Delete(customerId);
    }

    public IEnumerable<CustomerResultDto> GetAllCustomers()
    {
        var customers = _customerService.GetAllCustomers();

        foreach (Customer customer in customers)
        {
            var customerBankInfo = _customerBankInfoService.GetCustomerBankInfoByCustomerId(customer.Id);
            customer.CustomerBankInfo = _mapper.Map<CustomerBankInfo>(customerBankInfo);

            var portfolios = _portfolioService.GetPortfoliosByCustomerId(customer.Id);
            customer.Portfolios = _mapper.Map<List<Portfolio>>(portfolios);
        }

        return _mapper.Map<IEnumerable<CustomerResultDto>>(customers);
    }

    public CustomerResultDto GetCustomerById(long customerId)
    {
        var customer = _customerService.GetCustomerById(customerId);

        var customerBankInfo = _customerBankInfoService.GetCustomerBankInfoByCustomerId(customerId);
        customer.CustomerBankInfo = _mapper.Map<CustomerBankInfo>(customerBankInfo);

        var portfolios = _portfolioService.GetPortfoliosByCustomerId(customerId);
        customer.Portfolios = _mapper.Map<List<Portfolio>>(portfolios);

        return _mapper.Map<CustomerResultDto>(customer);
    }

    public void Update(long customerId, UpdateCustomerDto updateCustomerDto)
    {
        var customerMapeado = _mapper.Map<Customer>(updateCustomerDto);
        customerMapeado.Id = customerId;

        _customerService.Update(customerMapeado);
    }
}