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
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;

    public CustomerAppService(ICustomerService customerService, IMapper mapper)
    {
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public long Create(CreateCustomerDto createCustomerDto)
    {
        var customerMapeado = _mapper.Map<Customer>(createCustomerDto);

        return _customerService.Create(customerMapeado);
    }

    public void Delete(long customerId)
    {
        _customerService.Delete(customerId);
    }

    public IEnumerable<CustomerResultDto> GetAllCustomers()
    {
        var customers = _customerService.GetAllCustomers();

        return _mapper.Map<IEnumerable<CustomerResultDto>>(customers);
    }

    public CustomerResultDto GetCustomerById(long customerId)
    {
        var customer = _customerService.GetCustomerById(customerId);

        return _mapper.Map<CustomerResultDto>(customer);
    }

    public void Update(long customerId, UpdateCustomerDto updateCustomerDto)
    {
        var customerMapeado = _mapper.Map<Customer>(updateCustomerDto);
        customerMapeado.Id = customerId;

        _customerService.Update(customerMapeado);
    }
}