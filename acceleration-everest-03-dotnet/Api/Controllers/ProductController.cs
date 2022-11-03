using AppModels.Products;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
    public IActionResult Create(CreateProduct productToCreate)
    {
        try
        {
            var id = _productAppService.Create(productToCreate);

            return Created("Id:", id);
        }
        catch (Exception exception)
        {
            return BadRequest(exception);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        try
        {
            var result = await _productAppService.GetAllProductsAsync().ConfigureAwait(false);

            return Ok(result);
        }
        catch
        {
            return NoContent();
        }
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductByIdAsync(long productId)
    {
        try
        {
            var result = await _productAppService.GetProductByIdAsync(productId).ConfigureAwait(false);

            return Ok(result);
        }
        catch (ArgumentNullException exception)
        {
            var message = exception.InnerException?.Message ?? exception.Message;

            return NotFound(message);
        }
    }

    [HttpPut]
    public IActionResult Update(long productId, UpdateProduct productToUpdate)
    {
        try
        {
            _productAppService.Update(productId, productToUpdate);

            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception);
        }
    }
}