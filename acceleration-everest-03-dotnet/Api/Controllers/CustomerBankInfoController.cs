using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerBankInfoController : ControllerBase
{
    private readonly ICustomerBankInfoAppService _customerBankInfoAppService;

    public CustomerBankInfoController(ICustomerBankInfoAppService customerBankInfoAppService)
    {
        _customerBankInfoAppService = customerBankInfoAppService ?? throw new System.ArgumentNullException(nameof(customerBankInfoAppService));
    }

    [HttpPatch("depositInCustomerBankInfo/{customerId}")]
    public IActionResult Deposit(long customerId, decimal amount)
    {
        try
        {
            _customerBankInfoAppService.Deposit(customerId, amount);
            
            return Ok();
        }        
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            
            return NotFound(message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomersBankInfoAsync()
    {
        try
        {
            var result = await _customerBankInfoAppService.GetAllCustomersBankInfoAsync().ConfigureAwait(false);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            
            return NotFound(message);
        }
    }

    [HttpGet("getTotalByCustomerId/{customerBankInfoId}")]
    public IActionResult GetTotalByCustomerId(long customerBankInfoId)
    {
        try
        {
            var total = _customerBankInfoAppService.GetTotalByCustomerId(customerBankInfoId);
            return Ok(total);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
           
            return NotFound(message);
        }
    }

    [HttpPatch("withdraw/{customerId}")]
    public IActionResult Withdraw(long customerId, decimal amount)
    {
        try
        {
            _customerBankInfoAppService.Withdraw(customerId, amount);
            
            return Ok();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            
            return NotFound(message);
        }
    }
}