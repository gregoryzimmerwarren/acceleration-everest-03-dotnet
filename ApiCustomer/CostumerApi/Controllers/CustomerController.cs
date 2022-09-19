using AppServices.Interfaces;
using DomainModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
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

            return Ok();
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
        public IActionResult Post(CustomerModel customer)
        {
            try
            {
                _customerAppService.Create(customer);
                return Created("", customer.Id);
            }
            catch (ArgumentException exception)
            {
                var message = exception.InnerException?.Message ?? exception.Message;
                return BadRequest(message);
            }
        }

        [HttpPut]
        public IActionResult Update(CustomerModel customerToUpdate)
        {
            try
            {
                var result = _customerAppService.Update(customerToUpdate);

                if (!result)
                    return NotFound($"Did not found customer for Id: {customerToUpdate.Id}");

                return Ok();
            }
            catch (ArgumentException exception)
            {
                var message = exception.InnerException?.Message ?? exception.Message;
                return NotFound(message);
            }
        }
    }
}
