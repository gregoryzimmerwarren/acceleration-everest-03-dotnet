using AppModels.CustomersBankInfo;
using AppServices.Interfaces;
using AutoMapper;
using DomainServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Services;

public class CustomerBankInfoAppService : ICustomerBankInfoAppService
{
    private readonly ICustomerBankInfoService _customerBankInfoService;
    private readonly IMapper _mapper;

    public CustomerBankInfoAppService(
        ICustomerBankInfoService customerBankInfoService,
        IMapper mapper)
    {
        _customerBankInfoService = customerBankInfoService ?? throw new System.ArgumentNullException(nameof(customerBankInfoService));
        _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    public void Create(long customerId)
    {
        _customerBankInfoService.Create(customerId);
    }

    public async Task DeleteAsync(long customerId)
    {
        await _customerBankInfoService.DeleteAsync(customerId).ConfigureAwait(false);
    }

    public async Task DepositAsync(long customerId, decimal amount)
    {
        await _customerBankInfoService.DepositAsync(customerId, amount).ConfigureAwait(false);
    }

    public async Task<IEnumerable<CustomerBankInfoResult>> GetAllCustomersBankInfoAsync()
    {
        var customersBankInfo = await _customerBankInfoService.GetAllCustomersBankInfoAsync().ConfigureAwait(false);

        return _mapper.Map<IEnumerable<CustomerBankInfoResult>>(customersBankInfo);
    }

    public async Task<decimal> GetAccountBalanceByCustomerIdAsync(long customerId)
    {
        return await _customerBankInfoService.GetAccountBalanceByCustomerIdAsync(customerId).ConfigureAwait(false);
    }

    public async Task<bool> WithdrawAsync(long customerId, decimal amount)
    {
        var result = await _customerBankInfoService.WithdrawAsync(customerId, amount).ConfigureAwait(false);

        return result;
    }
}