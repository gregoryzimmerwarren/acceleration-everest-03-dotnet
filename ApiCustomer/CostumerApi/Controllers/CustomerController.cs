using AppModels.DTOs;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CustomerApi.Controllers;

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
        var result = _customerAppService.Delete(id);

        if (!result)
            return NotFound($"Did not found customer for Id: {id}");

        return NoContent();
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
        var result = _customerAppService.GetById(id);

        if (result == null)
            return NotFound($"Did not found customer for Id: {id}");

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Post(PostCustomerDto customer)
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
    public IActionResult Update(long id, PutCustomerDto customerToUpdate)
    {
        try
        {
            var result = _customerAppService.Update(id, customerToUpdate);

            if (!result)
                return NotFound($"Did not found customer for Id: {id}");

            return Ok();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;
            return NotFound(message);
        }
    }
}
