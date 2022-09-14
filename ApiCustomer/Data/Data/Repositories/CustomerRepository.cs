using Data.Entities;
using Data.Validator;

namespace Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<CustomerEntity> _customersList = new();

        public void Create(CustomerEntity customerToCreate)
        {
            var cpfExists = _customersList.Any(customer => customer.Cpf == customerToCreate.Cpf);
            var emailExists = _customersList.Any(customer => customer.Email == customerToCreate.Email);

            if (cpfExists == false && emailExists == true)
            {
                throw new ArgumentException("Email is already registered");
            }

            if (cpfExists == true && emailExists == false)
            {
                throw new ArgumentException("Cpf is already registered");
            }

            if (cpfExists == true && emailExists == true)
            {
                throw new ArgumentException("Cpf and Email are already registered");
            }

            customerToCreate.Id = _customersList.Count + 1;

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

        public bool Update(CustomerEntity entityToUpdate)
        {
            var cpfExists = _customersList.Any(customer => customer.Cpf == entityToUpdate.Cpf);
            var emailExists = _customersList.Any(customer => customer.Email == entityToUpdate.Email);

            if (cpfExists == false && emailExists == true)
            {
                throw new ArgumentException($"Did not found customer for Cpf: {entityToUpdate.Cpf}");
            }

            if (cpfExists == true && emailExists == false)
            {
                throw new ArgumentException($"Did not found customer for Email: {entityToUpdate.Email}"); ;
            }

            var customer = _customersList.Any(customer => customer.Id == entityToUpdate.Id);

            if (customer == false)
            {
                return false;
            }

            var index = _customersList.FindIndex(customer => customer.Id == entityToUpdate.Id);

            entityToUpdate.Id = _customersList[index].Id;

            _customersList[index] = entityToUpdate;

            return true;
        }
    }
}

