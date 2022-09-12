using Data.Entities;

namespace Data.Repositories
{
    public interface IBaseRepository<TEntity> 
        where TEntity : BaseEntity
    {
        bool CpfNotFound(TEntity entityToUpdate);
        bool EmailNotFound(TEntity entityToUpdate);
        string Create(TEntity entity);
        string Delete(long id);
        string Update(TEntity entity);
        List<TEntity> GetAll();
        TEntity GetById(long id);
    }
}