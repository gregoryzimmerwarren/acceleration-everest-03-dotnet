using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        if (EmailAlreadyExistsAsync(customerToCreate).Result)
        {
            var id = GetIdByEmailAsync(customerToCreate.Email).Result;
            throw new ArgumentException($"Email: {customerToCreate.Email} is already registered for Id: {id}");
        }

        if (CpfAlreadyExistsAsync(customerToCreate).Result)
        {
            var id = GetIdByCpfAsync(customerToCreate.Cpf).Result;
            throw new ArgumentException($"Cpf: {customerToCreate.Cpf} is already registered for Id: {id}"); ;
        }

        var repository = _unitOfWork.Repository<Customer>();
        repository.AddAsync(customerToCreate);
        _unitOfWork.SaveChangesAsync();

        return customerToCreate.Id;
    }

    public void Delete(long id)
    {
        var repository = _unitOfWork.Repository<Customer>();
        repository.Remove(customer);
        _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var repository = _repositoryFactory.Repository<Customer>();
        var query = repository.MultipleResultQuery()
            .Include(customer => customer.Include(customerBankInfo => customerBankInfo.CustomerBankInfo)
            .Include(portfolios => portfolios.Portfolios)); ;
        var customers = await repository.SearchAsync(query).ConfigureAwait(false);

        if (customers.Count == 0)
            throw new ArgumentException($"No customer found");

        return customers;
    }

    public async Task<Customer> GetCustomerByIdAsync(long id)
    {
        var repository = _repositoryFactory.Repository<Customer>();
        var query = repository.SingleResultQuery().AndFilter(customer => customer.Id == id)
            .Include(customer => customer.Include(customerBankInfo => customerBankInfo.CustomerBankInfo)
            .Include(portfolios => portfolios.Portfolios));
        var customer =await  repository.SingleOrDefaultAsync(query).ConfigureAwait(false);

        if (customer == null)
            throw new ArgumentException($"No customer found for Id: {id}");

        return customer;
    }

    public void Update(Customer customerToUpdate)
    {
        var repository = _unitOfWork.Repository<Customer>();

        if (!repository.Any(customer => customer.Id == customerToUpdate.Id))
            throw new ArgumentException($"No customer found for Id: {customerToUpdate.Id}");

        if (EmailAlreadyExistsAsync(customerToUpdate).Result)
        {
            var existingId = GetIdByEmailAsync(customerToUpdate.Email).Result;
            throw new ArgumentException($"Email: {customerToUpdate.Email} is already registered for Id: {existingId}");
        }

        if (CpfAlreadyExistsAsync(customerToUpdate).Result)
        {
            var existingId = GetIdByCpfAsync(customerToUpdate.Cpf).Result;
            throw new ArgumentException($"Cpf: {customerToUpdate.Cpf} is already registered for Id: {existingId}");
        }

        repository.Update(customerToUpdate);
        _unitOfWork.SaveChanges();
    }

    private async Task<bool> EmailAlreadyExistsAsync(Customer customerToCheck)
    {
        var repository = _repositoryFactory.Repository<Customer>();
        var query = await repository.AnyAsync(customer => customer.Email == customerToCheck.Email && customer.Id != customerToCheck.Id).ConfigureAwait(false);

        return query;
    }

    private async Task<bool> CpfAlreadyExistsAsync(Customer customerToCheck)
    {
        var repository = _repositoryFactory.Repository<Customer>();
        var query = await repository.AnyAsync(customer => customer.Cpf == customerToCheck.Cpf && customer.Id != customerToCheck.Id).ConfigureAwait(false);

        return query;
    }

    private async Task<long> GetIdByCpfAsync(string cpf)
    {
        var repository = _repositoryFactory.Repository<Customer>();
        var query = repository.SingleResultQuery().AndFilter(customer => customer.Cpf == cpf);
        var customer = await repository.SingleOrDefaultAsync(query).ConfigureAwait(false);

        if (customer == null)
            throw new ArgumentException($"No customer found for Cpf: {cpf}");

        return customer.Id;
    }

    private async Task<long> GetIdByEmailAsync(string email)
    {
        var repository = _repositoryFactory.Repository<Customer>();
        var query = repository.SingleResultQuery().AndFilter(customer => customer.Email == email);
        var customer = await repository.SingleOrDefaultAsync(query).ConfigureAwait(false);

        if (customer == null)
            throw new ArgumentException($"No customer found for Email: {email}");

        return customer.Id;
    }
}