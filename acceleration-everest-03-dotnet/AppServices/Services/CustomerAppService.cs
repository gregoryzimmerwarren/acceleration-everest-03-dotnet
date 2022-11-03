using AppModels.Customers;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Services;
public class CustomerAppService : ICustomerAppService
{
    private readonly ICustomerBankInfoAppService _customerBankInfoAppService;
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;

    public CustomerAppService(
        ICustomerBankInfoAppService customerBankInfoAppService,
        ICustomerService customerService,
        IMapper mapper)
    {
        _customerBankInfoAppService = customerBankInfoAppService ?? throw new ArgumentNullException(nameof(customerBankInfoAppService));
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<long> CreateAsync(CreateCustomer createCustomerDto)
    {
        var mappedCustomer = _mapper.Map<Customer>(createCustomerDto);
        var newCustomerId = await _customerService.CreateAsync(mappedCustomer).ConfigureAwait(false);

        _customerBankInfoAppService.Create(newCustomerId);

        return newCustomerId;
    }

    public async Task DeleteAsync(long customerId)
    {
        await _customerBankInfoAppService.DeleteAsync(customerId).ConfigureAwait(false);
        await _customerService.DeleteAsync(customerId).ConfigureAwait(false);
    }

    public async Task<IEnumerable<CustomerResult>> GetAllCustomersAsync()
    {
        var customers = await _customerService.GetAllCustomersAsync().ConfigureAwait(false);

        return _mapper.Map<IEnumerable<CustomerResult>>(customers);
    }

    public async Task<CustomerResult> GetCustomerByIdAsync(long customerId)
    {
        var customer = await _customerService.GetCustomerByIdAsync(customerId).ConfigureAwait(false);

        return _mapper.Map<CustomerResult>(customer);
    }

    public async Task UpdateAsync(long customerId, UpdateCustomer updateCustomerDto)
    {
        var mappedCustomer = _mapper.Map<Customer>(updateCustomerDto);
        mappedCustomer.Id = customerId;

        await _customerService.UpdateAsync(mappedCustomer).ConfigureAwait(false);
    }
}