using Data.Entities;

namespace Data.Repositories
{
    public interface ICustomerRepository
    {
        bool CpfNotFound(CustomerEntity entityToUpdate);
        bool EmailNotFound(CustomerEntity entityToUpdate);
        int Create(CustomerEntity entity);
        int Delete(long id);
        int Update(CustomerEntity entity);
        List<CustomerEntity> GetAll();
        CustomerEntity GetById(long id);
    }
}