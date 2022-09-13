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
            _repository = repository;
        }

        readonly CustomerValidator _validator = new();

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var result = _repository.Delete(id);

            if (result == 404)
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
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(CustomerEntity entity)
        {
            var result = _validator.Validate(entity);
            var cpfNotFound = _repository.CpfNotFound(entity);
            var emailNotFound = _repository.CpfNotFound(entity);

            if (!result.IsValid)
            {
                return BadRequest(result.ToString());
            }
            else if (result.IsValid && cpfNotFound == false && emailNotFound == true)
            {
                return BadRequest("Cpf is already registered");
            }
            else if (result.IsValid && cpfNotFound == true && emailNotFound == false)
            {
                return BadRequest("Email is already registered");
            }
            else if (result.IsValid && cpfNotFound == false && emailNotFound == false)
            {
                return BadRequest("Cpf and Email are already registered");
            }
            else
            {
                _repository.Create(entity);
                return Created("", entity.Id);
            }                            
        }

        [HttpPut]
        public IActionResult Update(CustomerEntity entity)
        {
            return Ok(_repository.Update(entity));
        }
    }
}
