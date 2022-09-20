using AppModels;
using System.Collections.Generic;

namespace AppServices.Interfaces;

public interface ICustomerAppService
{
    long Create(CreateCustomerDto postCustomerDto);
    void Delete(long id);
    IEnumerable<ResultCustomerDto> GetAll();
    ResultCustomerDto GetById(long id);
    void Update(long id, UpdateCustomerDto putCustomerDto);
}