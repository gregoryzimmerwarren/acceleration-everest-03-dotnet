using Data.Entities;
using Data.Repositories;
using Data.Validator;
using Microsoft.AspNetCore.Mvc;

namespace CostumerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : GenericController<CustomerEntity, CustomerRepository>
    {
        private ICustomerRepository _repository;
        CustomerValidator _validator = new CustomerValidator();

        public CustomerController(CustomerRepository repository) : base(repository)
        {
            _repository = repository;
        }

        [HttpDelete]
        public override IActionResult Delete(long id)
        {
            var result = _repository.Delete(id);

            if (result == "Not found")
                return NotFound();


            return Ok(result);
        }

        [HttpGet("{id}")]
        public override IActionResult GetById(long id)
        {
            var result = _repository.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public override IActionResult Post(CustomerEntity entity)
        {
            var result = _validator.Validate(entity);
            var cpfNotFound = _repository.CpfNotFound(entity);
            var emailNotFound = _repository.CpfNotFound(entity);

            if (result.IsValid && cpfNotFound == true && emailNotFound == true)
            {
                _repository.Create(entity);
                return CreatedAtAction(nameof(GetById), new {id = entity.Id}, entity);
            }
            else if (!result.IsValid)
            {
                return BadRequest(result.ToString());
            }
            else if (cpfNotFound == false && emailNotFound == true)
            {
                return BadRequest("Cpf is already registered");
            }
            else if (cpfNotFound == true && emailNotFound == false)
            {
                return BadRequest("Email is already registered");
            }
            return BadRequest("Cpf and Email are already registered");
        }

        [HttpPut]
        public override IActionResult Update(CustomerEntity entity)
        {
            return base.Update(entity);
        }
    }
}
