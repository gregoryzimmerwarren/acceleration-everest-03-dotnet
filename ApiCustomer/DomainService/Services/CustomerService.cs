using DomainModels;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainServices.Services;

public class CustomerService : ICustomerService
{
    private readonly List<Customer> _customersList = new();

    public long Create(Customer customerToCreate)
    {
        var emailExists = _customersList.Any(customer => customer.Email == customerToCreate.Email);
        if (emailExists)
        {
            throw new ArgumentException("Email is already registered");
        }
        
        var cpfExists = _customersList.Any(customer => customer.Cpf == customerToCreate.Cpf);
        if (cpfExists)
        {
            throw new ArgumentException("Cpf is already registered");
        }

        customerToCreate.Id = _customersList.LastOrDefault()?.Id + 1 ?? 1; 

        _customersList.Add(customerToCreate);

        return customerToCreate.Id;
    }

    public bool Delete(long id)
    {
        var customer = GetById(id);

        if (customer == null)
            return false;

        _customersList.Remove(customer);

        return true;
    }

    public IEnumerable<Customer> GetAll()
    {
        return _customersList;
    }

    public Customer GetById(long id)
    {
        var customers = _customersList.FirstOrDefault(customer => customer.Id == id);

        return customers;
    }

    public bool Update(Customer customerToUpdate)
    {
        var index = _customersList.FindIndex(customer => customer.Id == customerToUpdate.Id);
        if(index == -1) return false;

        var emailExists = _customersList.Any(customer => customer.Email == customerToUpdate.Email && customer.Id != customerToUpdate.Id);
        if (emailExists)
        {
            throw new ArgumentException($"Did not found customer for Email: {customerToUpdate.Email}");
        }

        var cpfExists = _customersList.Any(customer => customer.Cpf == customerToUpdate.Cpf && customer.Id != customerToUpdate.Id);
        if (cpfExists)
        {
            throw new ArgumentException($"Did not found customer for Cpf: {customerToUpdate.Cpf}");
        }           

        customerToUpdate.Id = _customersList[index].Id;

        _customersList[index] = customerToUpdate;

        return true;
    }
}

