using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces;

public interface ICustomerService
{
    Task<long> CreateAsync(Customer customerToCreate);
    Task DeleteAsync(long id);
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerByIdAsync(long id);
    Task UpdateAsync(Customer customerToUpdate);
}