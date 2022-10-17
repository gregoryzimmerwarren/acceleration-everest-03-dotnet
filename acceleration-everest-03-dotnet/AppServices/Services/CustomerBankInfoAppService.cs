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

    public void Delete(long customerId)
    {
        _customerBankInfoService.Delete(customerId);
    }

    public void Deposit(long customerId, decimal amount)
    {
        _customerBankInfoService.Deposit(customerId, amount);
    }

    public async Task<IEnumerable<CustomerBankInfoResult>> GetAllCustomersBankInfoAsync()
    {
        var customersBankInfo = await _customerBankInfoService.GetAllCustomersBankInfoAsync().ConfigureAwait(false);

        return _mapper.Map<IEnumerable<CustomerBankInfoResult>>(customersBankInfo);
    }

    public async Task<CustomerBankInfoResult> GetCustomerBankInfoByCustomerIdAsync(long customerId)
    {
        var customerBankInfo = await _customerBankInfoService.GetCustomerBankInfoByCustomerIdAsync(customerId).ConfigureAwait(false);

        return _mapper.Map<CustomerBankInfoResult>(customerBankInfo);
    }

    public decimal GetTotalByCustomerId(long customerId)
    {
        return _customerBankInfoService.GetTotalByCustomerId(customerId);
    }

    public bool Withdraw(long customerId, decimal amount)
    {
        var result = _customerBankInfoService.Withdraw(customerId, amount);

        return result;
    }
}