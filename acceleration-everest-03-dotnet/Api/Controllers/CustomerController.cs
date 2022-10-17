using AppModels.Customers;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

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
    public IActionResult GetAllCustomers()
    {
        try
        {
            var result = _customerAppService.GetAllCustomers();

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            
            return NotFound(message);
        }
    }

    [HttpGet("{customerId}")]
    public IActionResult GetByCustomerId(long customerId)
    {
        try
        {
            var result = _customerAppService.GetCustomerById(customerId);
            
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