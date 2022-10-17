using AppModels.Customers;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerAppService _customerAppService;

    public CustomerController(ICustomerAppService appService)
    {
        _customerAppService = appService ?? throw new ArgumentNullException(nameof(appService));
    }

    [HttpPost]
    public IActionResult Create(CreateCustomer customerToCreate)
    {
        try
        {
            var id = _customerAppService.Create(customerToCreate);
            
            return Created("Id:", id);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            
            return BadRequest(message);
        }
    }

    [HttpDelete]
    public IActionResult Delete(long customerId)
    {
        try
        {
            _customerAppService.Delete(customerId);
            
            return NoContent();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            
            return NotFound(message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomersAsync()
    {
        try
        {
            var result = await _customerAppService.GetAllCustomersAsync().ConfigureAwait(false);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            
            return NotFound(message);
        }
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetByCustomerIdAsync(long customerId)
    {
        try
        {
            var result = await _customerAppService.GetCustomerByIdAsync(customerId).ConfigureAwait(false);
            
            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            
            return NotFound(message);
        }
    }

    [HttpPut]
    public IActionResult Update(long customerId, UpdateCustomer customerToUpdate)
    {
        try
        {
            _customerAppService.Update(customerId, customerToUpdate);
            
            return Ok();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            
            return NotFound(message);
        }
    }
}