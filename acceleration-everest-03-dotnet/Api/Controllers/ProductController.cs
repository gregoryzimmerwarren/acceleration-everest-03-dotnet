using AppModels.Products;
using AppServices.Interfaces;
using DomainModels.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductAppService _productAppService;

    public ProductController(IProductAppService productAppService)
    {
        _productAppService = productAppService ?? throw new System.ArgumentNullException(nameof(productAppService));
    }

    [HttpPost]
    public IActionResult Create(CreateProductDto productToCreate)
    {
        try
        {
            var id = _productAppService.Create(productToCreate);

            return Created("", id);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return BadRequest(message);
        }
    }

    [HttpDelete]
    public IActionResult Delete(long productId)
    {
        try
        {
            _productAppService.Delete(productId);

            return NoContent();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        try
        {
            var result = _productAppService.GetAllProducts();

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(long productId)
    {
        try
        {
            var result = _productAppService.GetProductById(productId);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpPut]
    public IActionResult Update(long productId, UpdateProductDto productToUpdate)
    {
        try
        {
            _productAppService.Update(productId, productToUpdate);

            return Ok();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }
}