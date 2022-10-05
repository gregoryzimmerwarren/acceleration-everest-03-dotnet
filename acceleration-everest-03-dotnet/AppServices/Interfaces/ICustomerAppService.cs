using AppModels.Customers;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface ICustomerAppService
{
    long Create(CreateCustomerDto postCustomerDto);
    void Delete(long customerId);
    IEnumerable<CustomerResultDto> GetAllCustomers();
    CustomerResultDto GetCustomerById(long customerId);
    void Update(long customerId, UpdateCustomerDto putCustomerDto);
}