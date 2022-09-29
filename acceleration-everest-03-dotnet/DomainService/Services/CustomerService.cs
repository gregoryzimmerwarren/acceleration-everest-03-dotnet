using DomainModels;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryFactory _repositoryFactory;

    public CustomerService(IUnitOfWork unitOfWork, IRepositoryFactory repositoryFactory)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
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
        _unitOfWork.SaveChanges();
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
        var repository = _repositoryFactory.Repository<Customer>();

        if (!repository.Any(customer => customer.Id == customerToUpdate.Id))
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

        _unitOfWork.Repository<Customer>().Update(customerToUpdate);
        _unitOfWork.SaveChanges();
    }

    private bool EmailAlreadyExists(Customer customerToCheck)
    {
        return _repositoryFactory.Repository<Customer>().Any(customer => customer.Email == customerToCheck.Email);
    }

    private bool CpfAlreadyExists(Customer customerToCheck)
    {
        return _repositoryFactory.Repository<Customer>().Any(customer => customer.Cpf == customerToCheck.Cpf);
    }

    private bool EmailAlreadyExistsInAnotherCustomer(Customer customerToCheck)
    {
        return _repositoryFactory.Repository<Customer>().Any(customer => customer.Email == customerToCheck.Email && customer.Id != customerToCheck.Id);
    }

    private bool CpfAlreadyExistsInAnotherCustomer(Customer customerToCheck)
    {
        return _repositoryFactory.Repository<Customer>().Any(customer => customer.Cpf == customerToCheck.Cpf && customer.Id != customerToCheck.Id);
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