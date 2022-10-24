using AppModels.Orders;
using AppModels.Portfolios;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
    public IActionResult Create(CreatePortfolio portfolioToCreate)
    {
        try
        {
            var id = _portifolioAppService.Create(portfolioToCreate);

            return Created("Id:", id);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(long portfolioId)
    {
        try
        {
            await _portifolioAppService.DeleteAsync(portfolioId).ConfigureAwait(false);

            return NoContent();
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

    [HttpPatch("depositInPortfolio/{customerId}")]
    public async Task<IActionResult> DepositAsync(long customerId, long portfolioId, decimal amount)
    {
        try
        {
            await _portifolioAppService.DepositAsync(customerId, portfolioId, amount).ConfigureAwait(false);

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

    [HttpGet("{portfolioId}")]
    public async Task<IActionResult> GetPortfolioByIdAsync(long portfolioId)
    {
        try
        {
            var result = await _portifolioAppService.GetPortfolioByIdAsync(portfolioId).ConfigureAwait(false);

            return Ok(result);
        }
        catch (ArgumentNullException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpGet("getPortfoliosByCustomerId/{customerId}")]
    public async Task<IActionResult> GetPortfoliosByCustomerIdAsync(long customerId)
    {
        try
        {
            var result = await _portifolioAppService.GetPortfoliosByCustomerIdAsync(customerId).ConfigureAwait(false);

            return Ok(result);
        }
        catch (ArgumentNullException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpPatch("invest/{customerId}")]
    public async Task<IActionResult> InvestAsync(CreateOrder createOrderDto)
    {
        try
        {
            await _portifolioAppService.InvestAsync(createOrderDto).ConfigureAwait(false);

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

    [HttpPatch("redeemToPortfolio/{customerId}")]
    public async Task<IActionResult> RedeemToPortfolioAsync(CreateOrder createOrderDto)
    {
        try
        {
            await _portifolioAppService.RedeemToPortfolioAsync(createOrderDto).ConfigureAwait(false);

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

    [HttpPatch("withdrawFromPortfolio/{customerId}")]
    public async Task<IActionResult> WithdrawFromPortfolioAsync(long customerId, long portfolioId, decimal amount)
    {
        try
        {
            await _portifolioAppService.WithdrawFromPortfolioAsync(customerId, portfolioId, amount).ConfigureAwait(false);

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