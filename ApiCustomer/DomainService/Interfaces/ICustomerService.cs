using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Interfaces;

public interface ICustomerService
{
    long Create(CustomerModel customerToCreate);
    bool Delete(long id);
    List<CustomerModel> GetAll();
    CustomerModel GetById(long id);
    bool Update(CustomerModel customerToUpdate);
}