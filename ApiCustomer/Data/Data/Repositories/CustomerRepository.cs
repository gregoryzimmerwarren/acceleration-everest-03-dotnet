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

            if (cpfExists == false && emailpfExists == true)
            {
                return "4091";
            }
            else if (cpfExists == true && emailpfExists == false)
            {
                return "4092";
            }
            else
            {
                return "4093";
            }

            entity.Id = _customersList.Count + 1;

            _customersList.Add(entity);

            return "201";

        }

        public bool Delete(long id)
        {
            var entity = GetById(id);

            if (entity == null)
                return false;

            _customersList.Remove(entity);

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

        public bool Update(CustomerEntity entityToUpdate)
        {
            var index = _customersList.FindIndex(customer => customer.Cpf == entityToUpdate.Cpf && customer.Email == entityToUpdate.Email);

            if (index == -1)
            {
                return false;
            }          

            entityToUpdate.Id = _customersList[index].Id;

            _customersList[index] = entityToUpdate;

            return true;
        }
    }

