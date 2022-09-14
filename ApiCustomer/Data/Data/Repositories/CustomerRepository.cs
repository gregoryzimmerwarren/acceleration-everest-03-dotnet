using Data.Entities;
using Data.Validator;

namespace Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<CustomerEntity> _customersList = new();

        public string Create(CustomerEntity entity)
        {
            var customer = _customersList.Where(customer => customer.Cpf == entity.Cpf || customer.Email == entity.Email).FirstOrDefault();

            if (customer != null)
            {
                var cpfNotFound = CpfNotFound(entity);
                var emailNotFound = EmailNotFound(entity);

                if (cpfNotFound == false && emailNotFound == true)
                {
                    return "4091";
                }
                else if (cpfNotFound == true && emailNotFound == false)
                {
                    return "4092";
                }
                else
                {
                    return "4093";
                }
            }

            entity.Id = _customersList.Count + 1;

            _customersList.Add(entity);

            return "201";

        }

        public bool CpfNotFound(CustomerEntity entityToUpdate)
        {
            var customer = _customersList.Where(customer => customer.Cpf == entityToUpdate.Cpf);

            if (customer == null)
                return false;

            return true;
        }

        public string Delete(long id)
        {
            var entity = GetById(id);

            if (entity == null)
                return "404";

            _customersList.Remove(entity);

            return "200";
        }

        public bool EmailNotFound(CustomerEntity entityToUpdate)
        {
            var customer = _customersList.Where(customer => customer.Email == entityToUpdate.Email);

            if (customer == null)
                return false;

            return true;
        }

        public List<CustomerEntity> GetAll()
        {
            return _customersList;
        }

        public CustomerEntity GetById(long id)
        {
            var entity = _customersList.FirstOrDefault(customer => customer.Id == id);

            return entity;
        }

        public string Update(CustomerEntity entityToUpdate)
        {
            var entity = _customersList.Where(customer => customer.Cpf == entityToUpdate.Cpf || customer.Email == entityToUpdate.Email).FirstOrDefault();

            if (entity != null)
            {
                entityToUpdate.Cpf = entityToUpdate.Cpf.Trim().Replace(".", "").Replace("-", "");

                entityToUpdate.Id = entity.Id;

                var index = _customersList.IndexOf(entity);

                _customersList[index] = entityToUpdate;

                return "200";
            }

            return "404";
        }
    }
}
