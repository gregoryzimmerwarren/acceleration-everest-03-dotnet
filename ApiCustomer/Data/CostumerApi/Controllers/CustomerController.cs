using Data.Entities;
using Data.Repositories;
using Data.Validator;
using Microsoft.AspNetCore.Mvc;

namespace CostumerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var result = _repository.Delete(id);

            if (!result)
                return NotFound($"Did not found customer for Id: {id}");

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _repository.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var result = _repository.GetById(id);

            if (result == null)
                return NotFound($"Did not found customer for Id: {id}");

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(CustomerEntity customer)
        {
            try
            {
                _repository.Create(customer);
                return Created("", customer.Id);
            }
            catch (ArgumentException exception)
            {
                var message = exception.InnerException?.Message ?? exception.Message;
                return BadRequest(message);
            }
        }

        [HttpPut]
        public IActionResult Update(CustomerEntity customerToUpdate)
        {
            try
            {
                var result = _repository.Update(customerToUpdate);

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
