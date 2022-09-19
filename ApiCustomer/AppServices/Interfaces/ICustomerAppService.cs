using AppModels;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface ICustomerAppService
{
    long Create(CreateCustomerDto postCustomerDto);
    bool Delete(long id);
    IEnumerable<ResultCustomerDto> GetAll();
    ResultCustomerDto GetById(long id);
    bool Update(long id, UpdateCustomerDto putCustomerDto);
}