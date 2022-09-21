using AppModels;
using AppServices.Extensions;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels;
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

    public long Create(CreateCustomerDto postCustomerDto)
    {
        postCustomerDto.Cpf = postCustomerDto.Cpf.FormatCpf();
        var customerMapeado = _mapper.Map<Customer>(postCustomerDto);
        
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

    public void Update(long id, UpdateCustomerDto putCustomerDto)
    {
        var customerMapeado = _mapper.Map<Customer>(putCustomerDto);
        customerMapeado.Id = id;

        _customerService.Update(id, customerMapeado);
    }
}