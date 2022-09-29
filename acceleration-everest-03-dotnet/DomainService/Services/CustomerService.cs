using DomainModels;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace DomainServices.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryFactory _repositoryFactory;

    public CustomerService(
        IUnitOfWork<WarrenEverestDotnetDbContext> unitOfWork, 
        IRepositoryFactory<WarrenEverestDotnetDbContext> repositoryFactory)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _repositoryFactory = repositoryFactory ?? (IRepositoryFactory)_unitOfWork;
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
            var id = GetIdByCpf(customerToCreate.Cpf);
            throw new ArgumentException($"Cpf: {customerToCreate.Cpf} is already registered for Id: {id}"); ;
        }

        var repository = _unitOfWork.Repository<Customer>();
        repository.Add(customerToCreate);
        _unitOfWork.SaveChanges();

        return customerToCreate.Id;
    }

    public void Delete(long id)
    {
        var customer = GetById(id);

        if (customer == null)
            throw new ArgumentException($"Did not found customer for Id: {id}");

        var repository = _unitOfWork.Repository<Customer>();
        repository.Remove(customer);
    }

    public IEnumerable<Customer> GetAll()
    {
        var repository = _repositoryFactory.Repository<Customer>();
        var query = repository.MultipleResultQuery();

        return repository.Search(query);
    }

    public Customer GetById(long id)
    {
        var repository = _repositoryFactory.Repository<Customer>();
        var query = repository.SingleResultQuery().AndFilter(customer => customer.Id == id);
        var customer =  repository.SingleOrDefault(query);

        if (customer == null)
            throw new ArgumentException($"Did not found customer for Id: {id}");

        return customer;
    }

    public void Update(Customer customerToUpdate)
    {
        var repository = _unitOfWork.Repository<Customer>();

        if (!repository.Any(customer => customer.Id == customerToUpdate.Id))
            throw new ArgumentException($"Did not found customer for Id: {customerToUpdate.Id}");

        if (EmailAlreadyExists(customerToUpdate))
        {
            var existingId = GetIdByEmail(customerToUpdate.Email);
            throw new ArgumentException($"Email: {customerToUpdate.Email} is already registered for Id: {existingId}");
        }

        if (CpfAlreadyExists(customerToUpdate))
        {
            var existingId = GetIdByCpf(customerToUpdate.Cpf);
            throw new ArgumentException($"Cpf: {customerToUpdate.Cpf} is already registered for Id: {existingId}");
        }
        
        repository.Update(customerToUpdate);
        _unitOfWork.SaveChanges();
    }

    private bool EmailAlreadyExists(Customer customerToCheck)
    {
        var repository = _repositoryFactory.Repository<Customer>();
        var query = repository.Any(customer => customer.Email == customerToCheck.Email && customer.Id != customerToCheck.Id);

        return query;       
    }

    private bool CpfAlreadyExists(Customer customerToCheck)
    {
        var repository = _repositoryFactory.Repository<Customer>();
        var query = repository.Any(customer => customer.Cpf == customerToCheck.Cpf && customer.Id != customerToCheck.Id);

        return query;
    }

    private long GetIdByCpf(string cpf)
    {
        var repository = _repositoryFactory.Repository<Customer>();
        var query = repository.SingleResultQuery().AndFilter(customer => customer.Cpf == cpf);
        var customer = repository.SingleOrDefault(query);

        if (customer == null)
            throw new ArgumentException($"Did not found customer for Cpf: {cpf}");

        return customer.Id;
    }

    private long GetIdByEmail(string email)
    {
        var repository = _repositoryFactory.Repository<Customer>();
        var query = repository.SingleResultQuery().AndFilter(customer => customer.Email == email);
        var customer = repository.SingleOrDefault(query);

        if (customer == null)
            throw new ArgumentException($"Did not found customer for Email: {email}");

        return customer.Id;
    }
}