using Data.Entities;

namespace Data.Repositories
{
    public interface ICustomerRepository
    {
        void Create(CustomerEntity customerToCreate);
        bool Delete(long id);
        List<CustomerEntity> GetAll();
        CustomerEntity GetById(long id);
        bool Update(CustomerEntity customerToUpdate);
    }
}