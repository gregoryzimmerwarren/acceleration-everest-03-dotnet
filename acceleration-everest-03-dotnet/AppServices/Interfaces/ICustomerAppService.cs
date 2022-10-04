using AppModels.Customers;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface ICustomerAppService
{
    long Create(CreateCustomerDto postCustomerDto);
    void Delete(long id);
    IEnumerable<CustomerResultDto> GetAllCustomers();
    CustomerResultDto GetById(long id);
    void Update(long id, UpdateCustomerDto putCustomerDto);
}