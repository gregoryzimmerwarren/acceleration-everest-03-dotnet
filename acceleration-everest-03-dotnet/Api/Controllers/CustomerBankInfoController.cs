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
    public async Task<IActionResult> DepositAsync(long customerId, decimal amount)
    {
        try
        {
            await _customerBankInfoAppService.DepositAsync(customerId, amount).ConfigureAwait(false);
            
            return Ok();
        }        
        catch (ArgumentNullException exception)
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
        catch (Exception exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return Problem(message);
        }
    }

    [HttpGet("getTotalByCustomerId/{customerId}")]
    public async Task<IActionResult> GetAccountBalanceByCustomerIdAsync(long customerId)
    {
        try
        {
            var total = await _customerBankInfoAppService.GetAccountBalanceByCustomerIdAsync(customerId).ConfigureAwait(false);
            return Ok(total);
        }
        catch (ArgumentNullException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
           
            return NotFound(message);
        }
    }

    [HttpPatch("withdraw/{customerId}")]
    public async Task<IActionResult> WithdrawAsync(long customerId, decimal amount)
    {
        try
        {
            await _customerBankInfoAppService.WithdrawAsync(customerId, amount).ConfigureAwait(false);
            
            return Ok();
        }
        catch (ArgumentNullException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            
            return NotFound(message);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            
            return BadRequest(message);
        }
    }
}