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

    public async Task<long> CreateAsync(Customer customerToCreate)
    {
        if (await EmailAlreadyExistsAsync(customerToCreate).ConfigureAwait(false))
        {
            var id = await GetIdByEmailAsync(customerToCreate.Email).ConfigureAwait(false);
            throw new ArgumentException($"Email: {customerToCreate.Email} is already registered for Id: {id}");
        }

        if (await CpfAlreadyExistsAsync(customerToCreate).ConfigureAwait(false))
        {
            var id = await GetIdByCpfAsync(customerToCreate.Cpf).ConfigureAwait(false);
            throw new ArgumentException($"Cpf: {customerToCreate.Cpf} is already registered for Id: {id}"); ;
        }

        var repository = _unitOfWork.Repository<Customer>();
        repository.Add(customerToCreate);
        _unitOfWork.SaveChanges();

        return customerToCreate.Id;
    }

    public async Task DeleteAsync(long id)
    {
        var customer = await GetCustomerByIdAsync(id).ConfigureAwait(false);
        var repository = _unitOfWork.Repository<Customer>();
        repository.Remove(customer);
        _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var repository = _repositoryFactory.Repository<Customer>();
        var query = repository.MultipleResultQuery()
            .Include(customer => customer.Include(customerBankInfo => customerBankInfo.CustomerBankInfo)
            .Include(portfolios => portfolios.Portfolios));
        var customers = await repository.SearchAsync(query).ConfigureAwait(false);

        if (customers == null)
            throw new ArgumentNullException($"No customer found");

        return customers;
    }

    public async Task<Customer> GetCustomerByIdAsync(long id)
    {
        var repository = _unitOfWork.Repository<Customer>();
        var query = repository.SingleResultQuery().AndFilter(customer => customer.Id == id)
            .Include(customer => customer.Include(customerBankInfo => customerBankInfo.CustomerBankInfo)
            .Include(portfolios => portfolios.Portfolios));
        var customer = await  repository.SingleOrDefaultAsync(query).ConfigureAwait(false);

        if (customer == null)
            throw new ArgumentNullException($"No customer found for Id: {id}");

        return customer;
    }

    public async Task UpdateAsync(Customer customerToUpdate)
    {
        var repository = _unitOfWork.Repository<Customer>();

        if (!repository.Any(customer => customer.Id == customerToUpdate.Id))
            throw new ArgumentNullException($"No customer found for Id: {customerToUpdate.Id}");

        if (await EmailAlreadyExistsAsync(customerToUpdate).ConfigureAwait(false))
        {
            var existingId = GetIdByEmailAsync(customerToUpdate.Email);
            throw new ArgumentException($"Email: {customerToUpdate.Email} is already registered for Id: {existingId}");
        }

        if (await CpfAlreadyExistsAsync(customerToUpdate).ConfigureAwait(false))
        {
            var existingId = GetIdByCpfAsync(customerToUpdate.Cpf);
            throw new ArgumentException($"Cpf: {customerToUpdate.Cpf} is already registered for Id: {existingId}");
        }

        repository.Update(customerToUpdate);
        _unitOfWork.SaveChanges();
    }

    private async Task<bool> EmailAlreadyExistsAsync(Customer customerToCheck)
    {
        var repository = _unitOfWork.Repository<Customer>();
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

        return customer.Id;
    }

    private async Task<long> GetIdByEmailAsync(string email)
    {
        var repository = _repositoryFactory.Repository<Customer>();
        var query = repository.SingleResultQuery().AndFilter(customer => customer.Email == email);
        var customer = await repository.SingleOrDefaultAsync(query).ConfigureAwait(false);

        return customer.Id;
    }
}