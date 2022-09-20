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
        if (EmailAlreadyExists(customerToCreate))
            throw new ArgumentException("Email is already registered");

        if (CpfAlreadyExists(customerToCreate))
            throw new ArgumentException("Cpf is already registered");

        customerToCreate.Id = _customersList.LastOrDefault()?.Id + 1 ?? 1;

        _customersList.Add(customerToCreate);

        return customerToCreate.Id;
    }

    public void Delete(long id)
    {
        var customer = GetById(id);

        if (customer == null)
            throw new ArgumentException($"Did not found customer for Id: {id}");

        _customersList.Remove(customer);
    }

    public IEnumerable<Customer> GetAll()
    {
        return _customersList;
    }

    public Customer GetById(long id)
    {
        var customer = _customersList.FirstOrDefault(customer => customer.Id == id);

        if (customer == null)
            throw new ArgumentException($"Did not found customer for Id: {id}");

        return customer;
    }

    public void Update(long id, Customer customerToUpdate)
    {
        var index = _customersList.FindIndex(customer => customer.Id == customerToUpdate.Id);
        if (index == -1)
            throw new ArgumentException($"Did not found customer for Id: {id}");

        if (_customersList.Any(customer => customer.Id != customerToUpdate.Id))
        {
            if (!EmailAlreadyExists(customerToUpdate))
                throw new ArgumentException($"Did not found customer for Email: {customerToUpdate.Email}");

            if (!CpfAlreadyExists(customerToUpdate))
                throw new ArgumentException($"Did not found customer for Cpf: {customerToUpdate.Cpf}");
        }

        customerToUpdate.Id = _customersList[index].Id;

        _customersList[index] = customerToUpdate;
    }

    private bool EmailAlreadyExists(Customer customerToCheck)
    {
        return _customersList.Any(customer => customer.Email == customerToCheck.Email);
        if (emailExists)
        {
            return true;
        }

        return false;
    }

    private bool CpfAlreadyExists(Customer customerToCheck)
    {
        return _customersList.Any(customer => customer.Cpf == customerToCheck.Cpf);
        if (cpfExists)
        {
            return true;
        }

        return false;
    }
}