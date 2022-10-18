using AppModels.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces;

public interface ICustomerAppService
{
    Task<long> CreateAsync(CreateCustomer postCustomerDto);
    Task DeleteAsync(long customerId);
    Task<IEnumerable<CustomerResult>> GetAllCustomersAsync();
    Task<CustomerResult> GetCustomerByIdAsync(long customerId);
    Task UpdateAsync(long customerId, UpdateCustomer putCustomerDto);
}