using AppModels.Customers;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface ICustomerAppService
{
    long Create(CreateCustomer postCustomerDto);
    void Delete(long customerId);
    IEnumerable<CustomerResult> GetAllCustomers();
    CustomerResult GetCustomerById(long customerId);
    void Update(long customerId, UpdateCustomer putCustomerDto);
}