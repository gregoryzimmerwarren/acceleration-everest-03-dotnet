using AppModels.DTOs;

namespace AppServices.Interfaces
{
    public interface ICustomerAppService
    {
        void Create(PostCustomerDto postCustomerDto);
        bool Delete(long id);
        List<GetCustomerDto> GetAll();
        GetCustomerDto GetById(long id);
        bool Update(PutCustomerDto putCustomerDto);
    }
}