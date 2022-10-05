namespace AppModels.CustomerBankInfos;

public class CreateCustomerBankInfoDto
{
    public CreateCustomerBankInfoDto(long customerId)
    {
        AccountBalance = 0;
        CustomerId = customerId;
    }

    public decimal AccountBalance { get; set; }
    public long CustomerId { get; set; }
}