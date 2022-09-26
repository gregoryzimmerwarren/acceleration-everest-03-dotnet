using DomainModels;
using DomainServices.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainServices.Services;

public class CustomerService : ICustomerService
{
    private readonly WarrenEverestDotnetDbContext _context;

    public CustomerService(WarrenEverestDotnetDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public long Create(Customer customerToCreate)
    {
        if (EmailAlreadyExists(customerToCreate))
        {
            var id = GetIdByEmail(customerToCreate.Email);
            throw new ArgumentException($"Email: {customerToCreate.Email} is already registered for Id: {id}");
        }

        if (CpfAlreadyExists(customerToCreate))
        {
            var id = GetIdByEmail(customerToCreate.Cpf);
            throw new ArgumentException($"Cpf: {customerToCreate.Cpf} is already registered for Id: {id}"); ;
        }

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

    public void Update(Customer customerToUpdate)
    {
        if (!_context.Set<Customer>().Any(customer => customer.Id == customerToUpdate.Id))
            throw new ArgumentException($"Did not found customer for Id: {customerToUpdate.Id}");

        if (EmailAlreadyExistsInAnotherCustomer(customerToUpdate))
        {
            var existingId = GetIdByEmail(customerToUpdate.Email);
            throw new ArgumentException($"Email: {customerToUpdate.Email} is already registered for Id: {existingId}");
        }

        if (CpfAlreadyExistsInAnotherCustomer(customerToUpdate))
        {
            var existingId = GetIdByCpf(customerToUpdate.Cpf);
            throw new ArgumentException($"Cpf: {customerToUpdate.Cpf} is already registered for Id: {existingId}");
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

    private bool EmailAlreadyExistsInAnotherCustomer(Customer customerToCheck)
    {
        return _context.Set<Customer>().Any(customer => customer.Email == customerToCheck.Email && customer.Id != customerToCheck.Id);
    }

    private bool CpfAlreadyExistsInAnotherCustomer(Customer customerToCheck)
    {
        return _context.Set<Customer>().Any(customer => customer.Cpf == customerToCheck.Cpf && customer.Id != customerToCheck.Id);
    }

    private long GetIdByCpf(string cpf)
    {
        var customer = _context.Set<Customer>().FirstOrDefault(customer => customer.Cpf == cpf);

        if (customer == null)
            throw new ArgumentException($"Did not found customer for Cpf: {cpf}");

        return customer.Id;
    }

    private long GetIdByEmail(string email)
    {
        var customer = _context.Set<Customer>().FirstOrDefault(customer => customer.Email == email);

        if (customer == null)
            throw new ArgumentException($"Did not found customer for Email: {email}");

        return customer.Id;
    }
}