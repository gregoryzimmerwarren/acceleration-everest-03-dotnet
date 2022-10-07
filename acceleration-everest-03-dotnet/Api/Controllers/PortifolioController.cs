using AppModels.Portfolios;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PortifolioController : ControllerBase
{
    private readonly IPortifolioAppService _portifolioAppService;

    public PortifolioController(IPortifolioAppService portifolioAppService)
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

    [HttpPatch("/deposit/{customerId}")]
    public IActionResult Deposit(long portfolioId, decimal amount)
    {
        try
        {
            _portifolioAppService.Deposit(portfolioId, amount);

            return Ok();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet]
    public IActionResult GetAllPortifolios()
    {
        try
        {
            var result = _portifolioAppService.GetAllPortifolios();

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetPortifolioById(long portfolioId)
    {
        try
        {
            var result = _portifolioAppService.GetPortifolioById(portfolioId);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet("/getPortifoliosByCustomerId/{id}")]
    public IActionResult GetPortifoliosByCustomerId(long customerId)
    {
        try
        {
            var result = _portifolioAppService.GetPortifoliosByCustomerId(customerId);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpPatch("/invest/{customerId}")]
    public IActionResult Invest(long portfolioId, decimal amount)
    {
        try
        {
            _portifolioAppService.Invest(portfolioId, amount);

            return Ok();
        }
        catch (ArgumentException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpPatch("/redeemToPortfolio/{customerId}")]
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

    [HttpPatch("/withdrawFromPortfolio/{customerId}")]
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