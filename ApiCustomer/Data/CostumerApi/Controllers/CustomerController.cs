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

        CustomerValidator _validator = new();

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var result = _repository.Delete(id);

            if (result == "404")
                return NotFound();


            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
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
        public IActionResult Post(CustomerEntity entity)
        {
            var result = _repository.Create(entity);

            try
            {
                return Created("", entity.Id);    
            }
            catch (Exception ex)
            {
                 var message = ex.InnerException?.Message ?? ex.Message
                 return BadRequest(message);
            }       
        }

        [HttpPut]
        public IActionResult Update(CustomerEntity entity)
        {
            var result = _repository.Update(entity);

            if (result == "404")
                return NotFound();

            return Ok(result);
        }
    }
}
