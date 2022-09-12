using Data.Entities;

namespace Data.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        int Create(TEntity entity);
        int Delete(int id);
        int Update(TEntity entity);
        List<TEntity> GetAll();
        TEntity GetById(int id);
    }
}