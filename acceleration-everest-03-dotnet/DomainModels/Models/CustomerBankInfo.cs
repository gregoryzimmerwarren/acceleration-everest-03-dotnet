namespace DomainModels.Models;

public class CustomerBankInfo : IEntity
{
    public CustomerBankInfo(
        long customerId,
        decimal accountBalance)
    {
        AccountBalance = accountBalance;
        CustomerId = customerId;
    }

    public long Id { get; set; }
    public decimal AccountBalance { get; set; }

    public long CustomerId { get; set; }
    public Customer Customer { get; set; }
}
