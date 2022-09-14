using Data.Entities;

namespace Data.Repositories
{
    public interface ICustomerRepository
    {
        string Create(CustomerEntity entity);
        bool Delete(long id);
        bool Update(CustomerEntity entity);
        List<CustomerEntity> GetAll();
        CustomerEntity GetById(long id);
    }
}