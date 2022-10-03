using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace DomainServices.Services
{
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

        public long Create(CustomerBankInfo customerBankInfoToCreate)
        {
            var repository = _unitOfWork.Repository<CustomerBankInfo>();
            repository.Add(customerBankInfoToCreate);
            _unitOfWork.SaveChanges();

            return customerBankInfoToCreate.CustomerId;
        }

        public void Deposit(long customerId, decimal amount)
        {
            var customerBankInfo = GetCustomerBankInfoByCustomerId(customerId);
            var newAccountBalance = customerBankInfo.AccountBalance + amount;
            customerBankInfo.AccountBalance = newAccountBalance;

            var repository = _unitOfWork.Repository<CustomerBankInfo>();
            repository.Update(customerBankInfo);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<CustomerBankInfo> GetAllCustomersBankInfo()
        {
            var repository = _repositoryFactory.Repository<CustomerBankInfo>();
            var query = repository.MultipleResultQuery();

            return repository.Search(query);
        }

        public decimal GetTotalById(long customerId)
        {
            var customerBankInfo = GetCustomerBankInfoByCustomerId(customerId);

            return customerBankInfo.AccountBalance;
        }

        public void Withdraw(long customerId, decimal amount)
        {
            var customerBankInfo = GetCustomerBankInfoByCustomerId(customerId);
            var newAccountBalance = customerBankInfo.AccountBalance - amount;
            customerBankInfo.AccountBalance = newAccountBalance;

            var repository = _unitOfWork.Repository<CustomerBankInfo>();
            repository.Update(customerBankInfo);
            _unitOfWork.SaveChanges();
        }

        private CustomerBankInfo GetCustomerBankInfoByCustomerId(long customerId)
        {
            var repository = _unitOfWork.Repository<CustomerBankInfo>();
            var query = repository.SingleResultQuery().AndFilter(customerBankInfo => customerBankInfo.CustomerId == customerId);
            var customerBankInfo = repository.SingleOrDefault(query);

            if (customerBankInfo == null)
                throw new ArgumentException($"No bank information found for customer Id: {customerId}");

            return customerBankInfo;
        }
    }
}