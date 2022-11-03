namespace AppModels.CustomersBankInfo;

public class CustomerBankInfoResultForOthersDtos
{
    protected CustomerBankInfoResultForOthersDtos() { }

    public CustomerBankInfoResultForOthersDtos(
        long id,
        decimal accountBalance)
    {
        Id = id;
        AccountBalance = accountBalance;
    }

    public long Id { get; set; }
    public decimal AccountBalance { get; set; }
}