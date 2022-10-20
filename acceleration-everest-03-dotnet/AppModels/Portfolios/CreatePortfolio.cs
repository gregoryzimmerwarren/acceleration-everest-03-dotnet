using System.Text.Json.Serialization;

namespace AppModels.Portfolios;

public class CreatePortfolio
{
    public CreatePortfolio(
        string name,
        string description,
        long customerId)
    {
        Name = name;
        Description = description;
        TotalBalance = 0;
        AccountBalance = 0;
        CustomerId = customerId;
    }

    [JsonIgnore]
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [JsonIgnore]
    public decimal TotalBalance { get; set; }
    [JsonIgnore]
    public decimal AccountBalance { get; set; }
    public long CustomerId { get; set; }
}