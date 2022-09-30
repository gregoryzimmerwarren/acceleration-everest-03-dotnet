using AppModels;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface ICustomerAppService
{
    long Create(CreateCustomerDto postCustomerDto);
    void Delete(long id);
    IEnumerable<CustomerResult> GetAllCustomers();
    CustomerResult GetById(long id);
    void Update(long id, UpdateCustomerDto putCustomerDto);
}