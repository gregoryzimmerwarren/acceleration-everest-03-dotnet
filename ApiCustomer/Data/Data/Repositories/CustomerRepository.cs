using Data.Entities;
using System.Diagnostics.Metrics;

namespace Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<CustomerEntity> _customersList = new();

        public void Create(CustomerEntity customerToCreate)
        {
            var emailExists = _customersList.Any(customer => customer.Email == customerToCreate.Email);
            if (emailExists)
            {
                throw new ArgumentException("Email is already registered");
            }
            
            var cpfExists = _customersList.Any(customer => customer.Cpf == customerToCreate.Cpf);
            if (cpfExists)
            {
                throw new ArgumentException("Cpf is already registered");
            }

            customerToCreate.Id = _customersList.LastOrDefault()?.Id + 1 ?? 1; 

            _customersList.Add(customerToCreate);
        }

        public bool Delete(long id)
        {
            var customer = GetById(id);

            if (customer == null)
                return false;

            _customersList.Remove(customer);

            return true;
        }

        public List<CustomerEntity> GetAll()
        {
            return _customersList;
        }

        public CustomerEntity GetById(long id)
        {
            var customers = _customersList.FirstOrDefault(customer => customer.Id == id);

            return customers;
        }

        public bool Update(CustomerEntity customerToUpdate)
        {
            var cpfExists = _customersList.Any(customer => customer.Cpf == customerToUpdate.Cpf && customer.Id != customerToUpdate.Id);
            var emailExists = _customersList.Any(customer => customer.Email == customerToUpdate.Email && customer.Id != customerToUpdate.Id);

            if (cpfExists == false && emailExists == true)
            {
                throw new ArgumentException($"Did not found customer for Cpf: {customerToUpdate.Cpf}");
            }

            if (cpfExists == true && emailExists == false)
            {
                throw new ArgumentException($"Did not found customer for Email: {customerToUpdate.Email}"); ;
            }

            var customer = _customersList.Any(customer => customer.Id == customerToUpdate.Id);

            if (customer == false)
            {
                return false;
            }

            var index = _customersList.FindIndex(customer => customer.Id == customerToUpdate.Id);

            customerToUpdate.Id = _customersList[index].Id;

            _customersList[index] = customerToUpdate;

            return true;
        }
    }
}

