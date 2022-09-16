using DomainModels.Models;

namespace AppServices.Interfaces
{
    public interface ICustomerAppService
    {
        void Create(CustomerModel customerToCreate);
        bool Delete(long id);
        List<CustomerModel> GetAll();
        CustomerModel GetById(long id);
        bool Update(CustomerModel customerToUpdate);
    }
}