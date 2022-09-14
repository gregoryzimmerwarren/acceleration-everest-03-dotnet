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
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var result = _repository.Delete(id);

            if (result == "404")
                return NotFound($"Did not found customer for Id: {id}");


            return Ok(result);
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
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(CustomerEntity entity)
        {
            var result = _repository.Create(entity);

            if (result == "4091")
            {
                return BadRequest("Cpf is already registered");
            }
            else if (result == "4092")
            {
                return BadRequest("Email is already registered");
            }
            else if (result == "4093")
            {
                return BadRequest("Cpf and Email are already registered");
            }
            else
            {                
                return Created("", entity.Id);
            }                            
        }

        [HttpPut]
        public IActionResult Update(CustomerEntity entity)
        {
            var result = _repository.Update(entity);

            if (result == "404")
                return NotFound($"Did not found customer for Id: {id}");

            return Ok(result);
        }
    }
}
