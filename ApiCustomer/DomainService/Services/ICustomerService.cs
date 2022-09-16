using DomainModels.Entities;

namespace DomainService.Services
{
    public interface ICustomerService
    {
        void Create(CustomerEntity customerToCreate);
        bool Delete(long id);
        List<CustomerEntity> GetAll();
        CustomerEntity GetById(long id);
        bool Update(CustomerEntity customerToUpdate);
    }
}