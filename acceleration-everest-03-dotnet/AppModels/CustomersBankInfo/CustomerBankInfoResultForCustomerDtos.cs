namespace AppModels.CustomersBankInfo;

public class CustomerBankInfoResultForCustomerDtos
{
    protected CustomerBankInfoResultForCustomerDtos() { }

    public CustomerBankInfoResultForCustomerDtos(
        long id,
        decimal accountBalance)
    {
        Id = id;
        AccountBalance = accountBalance;
    }

    public long Id { get; set; }
    public decimal AccountBalance { get; set; }
}