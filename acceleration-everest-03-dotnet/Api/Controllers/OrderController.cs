using AppModels.Orders;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderAppService _orderAppService;

    public OrderController(IOrderAppService orderAppService)
    {
        _orderAppService = orderAppService ?? throw new System.ArgumentNullException(nameof(orderAppService));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrdersAsync()
    {
        try
        {
            var result = await _orderAppService.GetAllOrdersAsync().ConfigureAwait(false);

            return Ok(result);
        }
        catch (Exception exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return Problem(message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderByIdAsync(long id)
    {
        try
        {
            var result = await _orderAppService.GetOrderByIdAsync(id).ConfigureAwait(false);

            return Ok(result);
        }
        catch (ArgumentNullException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet("getOrdersByPortfolioId/{portfolioId}")]
    public async Task<IActionResult> GetOrdersByPortfolioIdAsync(long portfolioId)
    {
        try
        {
            var result = await _orderAppService.GetOrdersByPortfolioIdAsync(portfolioId).ConfigureAwait(false);

            return Ok(result);
        }
        catch (ArgumentNullException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet("getOrdersByProductId/{productId}")]
    public async Task<IActionResult> GetOrdersByProductIdAsync(long productId)
    {
        try
        {
            var result = await _orderAppService.GetOrdersByProductIdAsync(productId).ConfigureAwait(false);

            return Ok(result);
        }
        catch (ArgumentNullException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }
}