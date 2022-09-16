using AppServices.Interfaces;
using DomainModels.Models;
using DomainService.Interfaces;

namespace AppServices.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerService _customerService;

        public CustomerAppService(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        public void Create(CustomerModel customerToCreate)
        {
            _customerService.Create(customerToCreate);
        }

        public bool Delete(long id)
        {
            return _customerService.Delete(id);
        }

        public List<CustomerModel> GetAll()
        {
            return _customerService.GetAll();
        }

        public CustomerModel GetById(long id)
        {
            return _customerService.GetById(id);
        }

        public bool Update(CustomerModel customerToUpdate)
        {
            return _customerService.Update(customerToUpdate);
        }
    }
}
