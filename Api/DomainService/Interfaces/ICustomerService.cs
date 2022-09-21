using DomainModels;
using System.Collections.Generic;

namespace DomainServices.Interfaces;

public interface ICustomerService
{
    long Create(Customer customerToCreate);
    void Delete(long id);
    IEnumerable<Customer> GetAll();
    Customer GetById(long id);
    void Update(long id, Customer customerToUpdate);
}