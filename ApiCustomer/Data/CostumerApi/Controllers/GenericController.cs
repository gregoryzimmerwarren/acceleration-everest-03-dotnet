using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CostumerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<TEntity, TRepository> : ControllerBase
        where TEntity : BaseEntity
        where TRepository : BaseRepository<TEntity>
    {
        private TRepository _repository;

        public GenericController(TRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public virtual IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public virtual IActionResult GetById(long id)
        {
            return Ok(_repository.GetById(id));
        }

        [HttpPost()]
        public virtual IActionResult Post(TEntity entity)
        {
            return CreatedAtAction($"Created {entity}", _repository.Create(entity));
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(long id)
        {
            return Ok(_repository.Delete(id));
        }

        [HttpPut()]
        public virtual IActionResult Update(TEntity entity)
        {
            return Ok(_repository.Update(entity));
        }
    }
}
