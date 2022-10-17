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

    public long Create(CreateCustomer createCustomerDto)
    {
        var mappedCustomer = _mapper.Map<Customer>(createCustomerDto);
        var newCustomerId = _customerService.Create(mappedCustomer);

        _customerBankInfoService.Create(newCustomerId);

        return newCustomerId;
    }

    public void Delete(long customerId)
    {
        _customerBankInfoService.Delete(customerId);
        _customerService.Delete(customerId);
    }

    public IEnumerable<CustomerResult> GetAllCustomers()
    {
        var customers = _customerService.GetAllCustomers();

        foreach (Customer customer in customers)
        {
            var customerBankInfo = _customerBankInfoService.GetCustomerBankInfoByCustomerId(customer.Id);
            customer.CustomerBankInfo = _mapper.Map<CustomerBankInfo>(customerBankInfo);

            var portfolios = _portfolioService.GetPortfoliosByCustomerId(customer.Id);
            customer.Portfolios = _mapper.Map<List<Portfolio>>(portfolios);
        }

        return _mapper.Map<IEnumerable<CustomerResult>>(customers);
    }

    public CustomerResult GetCustomerById(long customerId)
    {
        var customer = _customerService.GetCustomerById(customerId);

        var customerBankInfo = _customerBankInfoService.GetCustomerBankInfoByCustomerId(customerId);
        customer.CustomerBankInfo = _mapper.Map<CustomerBankInfo>(customerBankInfo);

        var portfolios = _portfolioService.GetPortfoliosByCustomerId(customerId);
        customer.Portfolios = _mapper.Map<List<Portfolio>>(portfolios);

        return _mapper.Map<CustomerResult>(customer);
    }

    public void Update(long customerId, UpdateCustomer updateCustomerDto)
    {
        var mappedCustomer = _mapper.Map<Customer>(updateCustomerDto);
        mappedCustomer.Id = customerId;

        _customerService.Update(mappedCustomer);
    }
}