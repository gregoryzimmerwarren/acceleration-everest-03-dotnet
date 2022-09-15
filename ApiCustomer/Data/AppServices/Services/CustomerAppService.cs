using DomainModels.Entities;
using DomainService.Services;

namespace AppServices.Services
{
    public class CustomerAppService : ICustomerAppService
    {

        private readonly ICustomerService _customerService;

        public CustomerAppService(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        public void Create(CustomerEntity customerToCreate)
        {
            _customerService.Create(customerToCreate);
        }

        public bool Delete(long id)
        {
            return _customerService.Delete(id);
        }

        public List<CustomerEntity> GetAll()
        {
            return _customerService.GetAll();
        }

        public CustomerEntity GetById(long id)
        {
            return _customerService.GetById(id);
        }

        public bool Update(CustomerEntity customerToUpdate)
        {
            return _customerService.Update(customerToUpdate);
        }
    }
}
