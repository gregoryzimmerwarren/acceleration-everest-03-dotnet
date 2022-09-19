using DomainModels;
using System.Collections.Generic;

namespace DomainServices.Interfaces;

public interface ICustomerService
{
    long Create(Customer customerToCreate);
    bool Delete(long id);
    IEnumerable<Customer> GetAll();
    Customer GetById(long id);
    bool Update(Customer customerToUpdate);
}