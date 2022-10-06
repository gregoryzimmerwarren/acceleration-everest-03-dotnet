using AppModels.CustomersBankInfo;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

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

    [HttpPost]
    public IActionResult Create(CreateCustomerBankInfoDto customerBankInfo)
    {
        try
        {
            var id = _customerBankInfoAppService.Create(customerBankInfo);
            
            return Created("", id);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return BadRequest(message);
        }
    }

    [HttpPatch("/deposit/{customerId}")]
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
    public IActionResult GetAllCustomersBankInfo()
    {
        try
        {
            var result = _customerBankInfoAppService.GetAllCustomersBankInfo();

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            return NotFound(message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetTotalById(long id)
    {
        try
        {
            var total = _customerBankInfoAppService.GetTotalById(id);
            return Ok(total);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            return NotFound(message);
        }
    }

    [HttpPatch("/withdraw/{customerId}")]
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