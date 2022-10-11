using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;

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

    public void Delete(long customerId)
    {
        var customerBankInfo = GetCustomerBankInfoByCustomerId(customerId);
        var repository = _unitOfWork.Repository<CustomerBankInfo>();
        repository.Remove(customerBankInfo);
    }

    public void Deposit(long customerId, decimal amount)
    {
        var customerBankInfo = GetCustomerBankInfoByCustomerId(customerId);
        customerBankInfo.AccountBalance += amount;

        var repository = _unitOfWork.Repository<CustomerBankInfo>();
        repository.Update(customerBankInfo);
        _unitOfWork.SaveChanges();
    }

    public IEnumerable<CustomerBankInfo> GetAllCustomersBankInfo()
    {
        var repository = _repositoryFactory.Repository<CustomerBankInfo>();
        var query = repository.MultipleResultQuery();
        var customersBankInfos = repository.Search(query);

        if (customersBankInfos.Count == 0)
            throw new ArgumentException($"No customers bank infos found");

        return customersBankInfos;
    }

    public CustomerBankInfo GetCustomerBankInfoByCustomerId(long customerId)
    {
        var repository = _unitOfWork.Repository<CustomerBankInfo>();
        var query = repository.SingleResultQuery().AndFilter(customerBankInfo => customerBankInfo.CustomerId == customerId);
        var customerBankInfo = repository.SingleOrDefault(query);

        if (customerBankInfo == null)
            throw new ArgumentException($"No customer found for Id: {customerId}");

        return customerBankInfo;
    }

    public decimal GetTotalById(long customerId)
    {
        var customerBankInfo = GetCustomerBankInfoByCustomerId(customerId);

        if (customerBankInfo == null)
            throw new ArgumentException($"No bank information found for customer Id: {customerId}");

        return customerBankInfo.AccountBalance;
    }

    public bool Withdraw(long customerId, decimal amount)
    {
        var customerBankInfo = GetCustomerBankInfoByCustomerId(customerId);

        if (customerBankInfo == null)
            throw new ArgumentException($"No bank information found for customer Id: {customerId}");

        if (customerBankInfo.AccountBalance < amount)
            throw new ArgumentException($"Customer bank info does not have sufficient balance for this withdraw. Current balance: R${customerBankInfo.AccountBalance}");

        customerBankInfo.AccountBalance -= amount;

        var repository = _unitOfWork.Repository<CustomerBankInfo>();
        repository.Update(customerBankInfo);
        _unitOfWork.SaveChanges();

        return true;
    }
}