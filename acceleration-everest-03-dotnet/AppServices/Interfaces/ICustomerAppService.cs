using AppModels.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces;

public interface ICustomerAppService
{
    long Create(CreateCustomer postCustomerDto);
    void Delete(long customerId);
    Task<IEnumerable<CustomerResult>> GetAllCustomersAsync();
    Task<CustomerResult> GetCustomerByIdAsync(long customerId);
    void Update(long customerId, UpdateCustomer putCustomerDto);
}