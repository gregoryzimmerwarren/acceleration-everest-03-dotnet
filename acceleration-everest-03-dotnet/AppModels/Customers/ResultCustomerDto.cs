using DomainModels.Models;
using System.Collections.Generic;

namespace AppModels.Customers;

public class CustomerResultDto
{
    protected CustomerResultDto() { }

    public CustomerResultDto(
        long id, 
        string fullName, 
        string email, 
        string cpf, 
        string cellphone, 
        string city, 
        string postalCode, 
        CustomerBankInfo customerBankInfo, 
        List<Portfolio> portfolios)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Cpf = cpf;
        Cellphone = cellphone;
        City = city;
        PostalCode = postalCode;
        CustomerBankInfo = customerBankInfo;
        Portfolios = portfolios;
    }

    public long Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public string Cellphone { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public CustomerBankInfo CustomerBankInfo { get; set; }
    public List<Portfolio> Portfolios { get; set; }
}