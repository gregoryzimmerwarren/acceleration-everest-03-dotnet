using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces;

public interface ICustomerService
{
    long Create(Customer customerToCreate);
    void Delete(long id);
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerByIdAsync(long id);
    void Update(Customer customerToUpdate);
}