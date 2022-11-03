using AppModels.Customers;
using System.Text.Json.Serialization;

namespace AppModels.CustomersBankInfo;

public class CustomerBankInfoResult
{
    protected CustomerBankInfoResult() { }

    public CustomerBankInfoResult(
        long id, 
        decimal accountBalance,
        CustomerResultForOtherDtos customer)
    {
        Id = id;
        AccountBalance = accountBalance;
        Customer = customer;
    }

    [JsonIgnore]
    public long Id { get; set; }
    public decimal AccountBalance { get; set; }
    public CustomerResultForOtherDtos Customer { get; set; }
}