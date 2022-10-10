using AppModels.Orders;
using AppModels.Portfolios;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly IPortfolioAppService _portifolioAppService;

    public PortfolioController(IPortfolioAppService portifolioAppService)
    {
        _portifolioAppService = portifolioAppService ?? throw new System.ArgumentNullException(nameof(portifolioAppService));
    }

    [HttpPost]
    public IActionResult Create(CreatePortfolioDto portfolioToCreate)
    {
        try
        {
            var id = _portifolioAppService.Create(portfolioToCreate);

            return Created("", id);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return BadRequest(message);
        }
    }

    [HttpDelete]
    public IActionResult Delete(long portfolioId)
    {
        try
        {
            _portifolioAppService.Delete(portfolioId);

            return NoContent();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpPatch("depositInPortfolio/{customerId}")]
    public IActionResult Deposit(long customerId, long portfolioId, decimal amount, bool amountInBankInfo)
    {
        try
        {
            _portifolioAppService.Deposit(customerId, portfolioId, amount, amountInBankInfo);

            return Ok();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet]
    public IActionResult GetAllPortfolios()
    {
        try
        {
            var result = _portifolioAppService.GetAllPortfolios();

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet("{portfolioId}")]
    public IActionResult GetPortfolioById(long portfolioId)
    {
        try
        {
            var result = _portifolioAppService.GetPortfolioById(portfolioId);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet("getPortfoliosByCustomerId/{id}")]
    public IActionResult GetPortfoliosByCustomerId(long customerId)
    {
        try
        {
            var result = _portifolioAppService.GetPortfoliosByCustomerId(customerId);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpPatch("invest/{customerId}")]
    public IActionResult Invest(CreateOrderDto createOrderDto, long portfolioId, long productId, decimal amount)
    {
        try
        {
            _portifolioAppService.Invest(createOrderDto, portfolioId, productId, amount);

            return Ok();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpPatch("redeemToPortfolio/{customerId}")]
    public IActionResult RedeemToPortfolio(long portfolioId, decimal amount)
    {
        try
        {
            _portifolioAppService.RedeemToPortfolio(portfolioId, amount);

            return Ok();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpPatch("withdrawFromPortfolio/{customerId}")]
    public IActionResult WithdrawFromPortfolio(long portfolioId, decimal amount)
    {
        try
        {
            _portifolioAppService.WithdrawFromPortfolio(portfolioId, amount);

            return Ok();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }
}