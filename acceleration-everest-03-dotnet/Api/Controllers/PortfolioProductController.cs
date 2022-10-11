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

    [HttpGet("getPortfolioProductByIds")]
    public IActionResult GetPortfolioProductByIds(long portfolioId, long ProductId)
    {
        try
        {
            var result = _portfolioProductAppServices.GetPortfolioProductByIds(portfolioId, ProductId);

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