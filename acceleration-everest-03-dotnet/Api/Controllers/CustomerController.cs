﻿using AppModels.Customers;
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
    public async Task<IActionResult> CreateAsync(CreateCustomer customerToCreate)
    {
        try
        {
            var id = await _customerAppService.CreateAsync(customerToCreate).ConfigureAwait(false); ;
            
            return Created("Id:", id);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            
            return BadRequest(message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(long customerId)
    {
        try
        {
            await _customerAppService.DeleteAsync(customerId).ConfigureAwait(false);

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
        catch
        {
            return NoContent();
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
        catch (ArgumentNullException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            
            return NotFound(message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(long customerId, UpdateCustomer customerToUpdate)
    {
        try
        {
            await _customerAppService.UpdateAsync(customerId, customerToUpdate).ConfigureAwait(false);
            
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