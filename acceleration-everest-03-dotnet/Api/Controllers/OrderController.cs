using AppModels.Orders;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

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

    [HttpPost]
    public IActionResult Create(CreateOrder orderToCreate)
    {
        try
        {
            var id = _orderAppService.Create(orderToCreate);

            return Created("Id:", id);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return BadRequest(message);
        }
    }

    [HttpGet]
    public IActionResult GetAllOrders()
    {
        try
        {
            var result = _orderAppService.GetAllOrders();

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet("{orderId}")]
    public IActionResult GetOrderById(long orderId)
    {
        try
        {
            var result = _orderAppService.GetOrderById(orderId);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet("getOrdersByPortfolioId/{portfolioId}")]
    public IActionResult GetOrdersByPortfolioId(long portfolioId)
    {
        try
        {
            var result = _orderAppService.GetOrdersByPortfolioId(portfolioId);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet("getOrdersByProductId/{productId}")]
    public IActionResult GetOrdersByProductId(long productId)
    {
        try
        {
            var result = _orderAppService.GetOrdersByProductId(productId);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }
}