using AppModels;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels;
using DomainServices.Interfaces;
using Infrastructure.CrossCutting.Extensions;
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

    public void Delete(long id)
    {
        _customerService.Delete(id);
    }

    public IEnumerable<CustomerResult> GetAll()
    {
        var customers = _customerService.GetAll();

        return _mapper.Map<IEnumerable<CustomerResult>>(customers);
    }

    public CustomerResult GetById(long id)
    {
        var customer = _customerService.GetById(id);

        return _mapper.Map<CustomerResult>(customer);
    }

    public void Update(long id, UpdateCustomerDto updateCustomerDto)
    {
        var customerMapeado = _mapper.Map<Customer>(updateCustomerDto);
        customerMapeado.Id = id;

        _customerService.Update(customerMapeado);
    }
}