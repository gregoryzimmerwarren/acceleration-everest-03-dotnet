using Data.Entities;

namespace Data.Repositories
{
    public class CustomerRepository : BaseRepository<CustomerEntity>, 
        ICustomerRepository
    {
        private List<CustomerEntity> _customerList;

        public CustomerRepository(List<CustomerEntity> entityList) : base(entityList)
        {
            _customerList = entityList;
        }

        public override string Create(CustomerEntity entity)
        {
            entity.Cpf = entity.Cpf.Trim().Replace(".", "").Replace("-", "");

            for (var i = 0; i < _customerList.Count; i++)
            {
                var customer = _customerList[i];

                if (customer.Cpf == entity.Cpf || customer.Email == entity.Email)
                {
                    return "Customer already exist";
                }
            }

            return Create(entity);
        }

        public override string Update(CustomerEntity entityToUpdate)
        {
            var entity = GetById(entityToUpdate.Id);

            if (entity == null)
                return "Not found";

            entityToUpdate.Cpf = entityToUpdate.Cpf.Trim().Replace(".", "").Replace("-", "");

            for (var i = 0; i < _customerList.Count; i++)
            {
                var customer = _customerList[i];

                if (customer.Cpf == entityToUpdate.Cpf && customer.Email == entityToUpdate.Email)
                {
                    entityToUpdate.Id = customer.Id;

                    return Update(entityToUpdate);
                }
            }

            return "Not found";
        }

        public override bool CpfNotFound(CustomerEntity entityToUpdate)
        {
            var customer = _customerList.Where(customer => customer.Cpf == entityToUpdate.Cpf).FirstOrDefault();

            if (customer != null)
                return false;

            return true;
        }

        public override bool EmailNotFound(CustomerEntity entityToUpdate)
        {
            var customer = _customerList.Where(customer => customer.Email == entityToUpdate.Email).FirstOrDefault();

            if (customer != null)
                return false;

            return true;
        }
    }
}
