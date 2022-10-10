using AppModels.PortfoliosProducts;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PortfolioProductController : ControllerBase
{
    private readonly IPortfolioProductAppService _portfolioProductAppServices;

    public PortfolioProductController(IPortfolioProductAppService portfolioProductAppServices)
    {
        _portfolioProductAppServices = portfolioProductAppServices ?? throw new System.ArgumentNullException(nameof(portfolioProductAppServices));
    }

    [HttpPost]
    public IActionResult Create(CreatePortfolioProductDto portfolioProductToCreate)
    {
        try
        {
            var id = _portfolioProductAppServices.Create(portfolioProductToCreate);

            return Created("", id);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return BadRequest(message);
        }
    }

    [HttpDelete]
    public IActionResult Delete(long portfolioProductId)
    {
        try
        {
            _portfolioProductAppServices.Delete(portfolioProductId);

            return NoContent();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet]
    public IActionResult GetAllPortfolioProduct()
    {
        try
        {
            var result = _portfolioProductAppServices.GetAllPortfolioProduct();

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet("getPortfolioProductById/{portfolioProductId}")]
    public IActionResult GetPortfolioProductById(long portfolioProductId)
    {
        try
        {
            var result = _portfolioProductAppServices.GetPortfolioProductById(portfolioProductId);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet("getPortfoliosByProductId/{productId}")]
    public IActionResult GetPortfoliosByProductId(long productId)
    {
        try
        {
            var result = _portfolioProductAppServices.GetPortfolioProductByProductId(productId);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet("getProductsByPortfolioId/{portfolioId}")]
    public IActionResult GetProductsByPortfolioId(long portfolioId)
    {
        try
        {
            var result = _portfolioProductAppServices.GetPortfolioProductByPortfolioId(portfolioId);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }
}