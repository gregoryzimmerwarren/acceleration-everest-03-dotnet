using DomainModels.Entities;

namespace AppServices.Services
{
    public interface ICustomerAppService
    {
        void Create(CustomerEntity customerToCreate);
        bool Delete(long id);
        List<CustomerEntity> GetAll();
        CustomerEntity GetById(long id);
        bool Update(CustomerEntity customerToUpdate);
    }
}