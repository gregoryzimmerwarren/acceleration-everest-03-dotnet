using AppModels.CustomersBankInfo;
using AppModels.Portfolios;
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
        CustomerBankInfoResultForOthersDtos customerBankInfo, 
        List<PortfolioResultForOthersDtos> portfolios)
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
    public CustomerBankInfoResultForOthersDtos CustomerBankInfo { get; set; }
    public List<PortfolioResultForOthersDtos> Portfolios { get; set; }
}