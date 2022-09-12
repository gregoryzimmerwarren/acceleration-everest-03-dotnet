using Data.Entities;

namespace Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly List<TEntity> _entityList;

        public BaseRepository(List<TEntity> entityList)
        {
            _entityList = entityList;
        }

        public virtual bool CpfNotFound(TEntity entityToUpdate)
        {
            return true;
        }

        public virtual string Create(TEntity entity)
        {
            entity.Id = _entityList.Count + 1;
            _entityList.Add(entity);

            return "Created";
        }

        public virtual string Delete(long id)
        {
            var entity = GetById(id);

            if (entity == null)
                return "Not found";

            _entityList.Remove(entity);

            return "Deleted";
        }

        public virtual bool EmailNotFound(TEntity entityToUpdate)
        {
            return true;
        }

        public virtual List<TEntity> GetAll()
        {
            return _entityList;
        }

        public virtual TEntity GetById(long id)
        {
            TEntity entity = _entityList.Where(customer => customer.Id == id).FirstOrDefault();

            if (entity == null)
                return null;

            return entity;
        }

        public virtual string Update(TEntity entityToUpdate)
        {
            return "Updated";
        }
    }
}
