using AppModels.Customers;

namespace AppModels.CustomersBankInfo;

public class CustomerBankInfoResultDto
{
    public CustomerBankInfoResultDto() { }

    public CustomerBankInfoResultDto(
        long id, 
        decimal accountBalance,
        CustomerResultForOtherDtos customer)
    {
        Id = id;
        AccountBalance = accountBalance;
        Customer = customer;
    }

    public long Id { get; set; }
    public decimal AccountBalance { get; set; }
    public CustomerResultForOtherDtos Customer { get; set; }
}