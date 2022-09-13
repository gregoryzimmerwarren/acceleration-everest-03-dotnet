using Data.Entities;
using Data.Validator;

namespace Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<CustomerEntity> _customersList = new();

        public int Create(CustomerEntity entity)
        {
            entity.Cpf = entity.Cpf.Trim().Replace(".", "").Replace("-", "");

            var exists = _customersList.Any(customer => customer.Cpf == entity.Cpf || customer.Email == entity.Email);

            if (exists)
                return 409;

            entity.Id = _customersList.Count + 1;

            _customersList.Add(entity);

            return 201;    

        }

        public bool CpfNotFound(CustomerEntity entityToUpdate)
        {
            var customer = _customersList.Where(customer => customer.Cpf == entityToUpdate.Cpf).FirstOrDefault();

            if (customer == null)
                return true;

            return false;
        }

        public int Delete(long id)
        {
            var entity = GetById(id);

            if (entity == null)
                return 404;

            _customersList.Remove(entity);

            return 200;
        }

        public bool EmailNotFound(CustomerEntity entityToUpdate)
        {
            var customer = _customersList.Where(customer => customer.Email == entityToUpdate.Email).FirstOrDefault();

            if (customer == null)
                return true;

            return false;
        }

        public List<CustomerEntity> GetAll()
        {
            return _customersList;
        }

        public CustomerEntity GetById(long id)
        {
            var entity = _customersList.Where(customer => customer.Id == id).FirstOrDefault();

            if (entity == null)
                return null;

            return entity;
        }

        public int Update(CustomerEntity entityToUpdate)
        {
            var entity = GetById(entityToUpdate.Id);

            if (entity == null)
                return 404;

            entityToUpdate.Cpf = entityToUpdate.Cpf.Trim().Replace(".", "").Replace("-", "");
          
            var customer = _customersList.Where(customer => customer.Cpf == entity.Cpf || customer.Email == entity.Email).FirstOrDefault();

            if (customer != null)
            {
                entityToUpdate.Id = customer.Id;

                var index = _customersList.IndexOf(customer);

                _customersList[index] = entityToUpdate;

                return 200;
            }

            return 404;
        }
    }
}
