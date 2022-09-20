using AppModels;
using AppServices.Interfaces;
using DomainModels;
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

    [HttpDelete]
    public IActionResult Delete(long id)
    {
        try
        {
            _customerAppService.Delete(id);
            return NoContent();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            return NotFound(message);
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _customerAppService.GetAll();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(long id)
    {
        try
        {
            var result = _customerAppService.GetById(id);
            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            return NotFound(message);
        }
    }

    [HttpPost]
    public IActionResult Post(CreateCustomerDto customer)
    {
        try
        {
            var id = _customerAppService.Create(customer);
            return Created("", id);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            return BadRequest(message);
        }
    }

    [HttpPut]
    public IActionResult Update(long id, UpdateCustomerDto customerToUpdate)
    {
        try
        {
            _customerAppService.Update(id, customerToUpdate);
            return Ok();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            return NotFound(message);
        }
    }
}