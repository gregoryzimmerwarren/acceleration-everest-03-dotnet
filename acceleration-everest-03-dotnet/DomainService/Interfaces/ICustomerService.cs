﻿using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Interfaces;

public interface ICustomerService
{
    long Create(Customer customerToCreate);
    void Delete(long id);
    IEnumerable<Customer> GetAllCustomers();
    Customer GetById(long id);
    void Update(Customer customerToUpdate);
}