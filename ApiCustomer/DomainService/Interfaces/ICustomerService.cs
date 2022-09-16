using DomainModels.Models;

namespace DomainServices.Interfaces
{
    public interface ICustomerService
    {
        void Create(CustomerModel customerToCreate);
        bool Delete(long id);
        List<CustomerModel> GetAll();
        CustomerModel GetById(long id);
        bool Update(CustomerModel customerToUpdate);
    }
}