﻿using AppModels.DTOs;

namespace AppServices.Interfaces
{
    public interface ICustomerAppService
    {
        long Create(PostCustomerDto postCustomerDto);
        bool Delete(long id);
        List<GetCustomerDto> GetAll();
        GetCustomerDto GetById(long id);
        bool Update(long id, PutCustomerDto putCustomerDto);
    }
}