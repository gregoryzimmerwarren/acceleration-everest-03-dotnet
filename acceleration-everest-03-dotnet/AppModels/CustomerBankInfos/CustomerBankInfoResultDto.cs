using DomainModels.Models;

namespace AppModels.CustomerBankInfos;

public class CustomerBankInfoResultDto
{
    public CustomerBankInfoResultDto() { }

    public CustomerBankInfoResultDto(
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