using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Services;

public class CustomerBankInfoService : ICustomerBankInfoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryFactory _repositoryFactory;

    public CustomerBankInfoService(
        IUnitOfWork<WarrenEverestDotnetDbContext> unitOfWork, 
        IRepositoryFactory<WarrenEverestDotnetDbContext> repositoryFactory)
    {
        _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
        _repositoryFactory = repositoryFactory ?? (IRepositoryFactory)_unitOfWork;
    }

    public void Create(long customerId)
    {
        var repository = _unitOfWork.Repository<CustomerBankInfo>();
        repository.Add(new CustomerBankInfo(customerId));
        _unitOfWork.SaveChanges();
    }

    public async Task DeleteAsync(long customerId)
    {
        var customerBankInfo = await GetCustomerBankInfoByCustomerIdAsync(customerId).ConfigureAwait(false);
        var repository = _unitOfWork.Repository<CustomerBankInfo>();
        repository.Remove(customerBankInfo);
        _unitOfWork.SaveChanges();
    }

    public async Task DepositAsync(long customerId, decimal amount)
    {
        var customerBankInfo = await GetCustomerBankInfoByCustomerIdAsync(customerId).ConfigureAwait(false);
        customerBankInfo.AccountBalance += amount;

        var repository = _unitOfWork.Repository<CustomerBankInfo>();
        repository.Update(customerBankInfo);
        _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<CustomerBankInfo>> GetAllCustomersBankInfoAsync()
    {
        var repository = _repositoryFactory.Repository<CustomerBankInfo>();
        var query = repository.MultipleResultQuery()
            .Include(bankInfo => bankInfo.Include(customer => customer.Customer));
        var customersBankInfos = await repository.SearchAsync(query).ConfigureAwait(false);

        if (customersBankInfos == null)
            throw new ArgumentNullException($"No customers bank infos found");

        return customersBankInfos;
    }

    public async Task<CustomerBankInfo> GetCustomerBankInfoByCustomerIdAsync(long customerId)
    {
        var repository = _unitOfWork.Repository<CustomerBankInfo>();
        var query = repository.SingleResultQuery().AndFilter(customerBankInfo => customerBankInfo.CustomerId == customerId)
            .Include(bankInfo => bankInfo.Include(customer => customer.Customer));
        var customerBankInfo = await repository.SingleOrDefaultAsync(query).ConfigureAwait(false);

        if (customerBankInfo == null)
            throw new ArgumentNullException($"No customer found for Id: {customerId}");

        return customerBankInfo;
    }

    public async Task<decimal> GetAccountBalanceByCustomerIdAsync(long customerId)
    {
        var customerBankInfo = await GetCustomerBankInfoByCustomerIdAsync(customerId).ConfigureAwait(false);

        return customerBankInfo.AccountBalance;
    }

    public async Task<bool> WithdrawAsync(long customerId, decimal amount)
    {
        var customerBankInfo = await GetCustomerBankInfoByCustomerIdAsync(customerId).ConfigureAwait(false);

        if (customerBankInfo.AccountBalance < amount)
            throw new ArgumentException($"Customer bank info does not have sufficient balance for this withdraw. Current balance: R${customerBankInfo.AccountBalance}");

        customerBankInfo.AccountBalance -= amount;

        var repository = _unitOfWork.Repository<CustomerBankInfo>();
        repository.Update(customerBankInfo);
        _unitOfWork.SaveChanges();

        return true;
    }
}