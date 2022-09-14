using Data.Entities;

namespace Data.Repositories
{
    public interface ICustomerRepository
    {
        bool CpfNotFound(CustomerEntity entityToUpdate);
        bool EmailNotFound(CustomerEntity entityToUpdate);
        string Create(CustomerEntity entity);
        string Delete(long id);
        string Update(CustomerEntity entity);
        List<CustomerEntity> GetAll();
        CustomerEntity GetById(long id);
    }
}