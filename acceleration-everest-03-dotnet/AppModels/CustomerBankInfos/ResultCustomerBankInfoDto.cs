using DomainModels.Models;

namespace AppModels.CustomerBankInfos;

internal class ResultCustomerBankInfoDto
{
    public ResultCustomerBankInfoDto() { }

    public ResultCustomerBankInfoDto(
        long id, 
        decimal accountBalance, 
        Customer customer)
    {
        Id = id;
        AccountBalance = accountBalance;
        Customer = customer;
    }

    public long Id { get; set; }
    public decimal AccountBalance { get; set; }
    public Customer Customer { get; set; }
}