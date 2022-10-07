using AppModels.CustomersBankInfo;
using AppModels.Products;
using System.Collections.Generic;

namespace AppModels.Customers;

public class CustomerResultForOtherDtos
{
    protected CustomerResultForOtherDtos() { }

    public CustomerResultForOtherDtos(
        long id,
        string fullName,
        string email,
        string cpf,
        string cellphone,
        string city,
        string postalCode)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Cpf = cpf;
        Cellphone = cellphone;
        City = city;
        PostalCode = postalCode;
    }

    public long Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public string Cellphone { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
}