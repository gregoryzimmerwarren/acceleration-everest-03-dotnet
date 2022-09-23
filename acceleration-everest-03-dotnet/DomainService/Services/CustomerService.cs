using DomainModels;
using DomainServices.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainServices.Services;

public class CustomerService : ICustomerService
{
    private readonly WarrenEverestDotnetDbContext _context;

    public CustomerService(WarrenEverestDotnetDbContext context)
    {
        _context = context;
    }

    public long Create(Customer customerToCreate)
    {        
        if (EmailAlreadyExists(customerToCreate))
            throw new ArgumentException($"Email: {customerToCreate.Email} is already registered for Id: {customerToCreate.Id}");
        
        if (CpfAlreadyExists(customerToCreate))
            throw new ArgumentException($"Cpf: {customerToCreate.Cpf} is already registered for Id: {customerToCreate.Id}");

        _context.Set<Customer>().Add(customerToCreate);
        _context.SaveChanges();

        return customerToCreate.Id;
    }

    public void Delete(long id)
    {
        var customer = GetById(id);

        if (customer == null)
            throw new ArgumentException($"Did not found customer for Id: {id}");

        _context.Set<Customer>().Remove(customer);
        _context.SaveChanges();
    }

    public IEnumerable<Customer> GetAll()
    {
        return _context.Set<Customer>().ToList();
    }

    public Customer GetById(long id)
    {
        var customer = _context.Set<Customer>().FirstOrDefault(customer => customer.Id == id);

        if (customer == null)
            throw new ArgumentException($"Did not found customer for Id: {id}");

        return customer;
    }

    public void Update(long id, Customer customerToUpdate)
    {
        var customer = _context.Set<Customer>().Any(customer => customer.Id == customerToUpdate.Id);
        
        if (!customer)
            throw new ArgumentException($"Did not found customer for Id: {id}");

        if (_context.Set<Customer>().Any(customer => customer.Id != customerToUpdate.Id))
        {
            if (EmailAlreadyExists(customerToUpdate))
                throw new ArgumentException($"Email: {customerToUpdate.Email} is already registered in another id than the Id: {customerToUpdate.Id}");
            
            if (CpfAlreadyExists(customerToUpdate))
                throw new ArgumentException($"Cpf: {customerToUpdate.Cpf} is already registered in another id than the Id: {customerToUpdate.Id}");
        }

        _context.Set<Customer>().Update(customerToUpdate);
        _context.SaveChanges();
    }

    private bool EmailAlreadyExists(Customer customerToCheck)
    {
        return _context.Set<Customer>().Any(customer => customer.Email == customerToCheck.Email);        
    }

    private bool CpfAlreadyExists(Customer customerToCheck)
    {
        return _context.Set<Customer>().Any(customer => customer.Cpf == customerToCheck.Cpf);        
    }
}