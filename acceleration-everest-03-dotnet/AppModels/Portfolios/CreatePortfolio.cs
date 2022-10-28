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
        CustomerId = customerId;
    }

    [JsonIgnore]
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }   
    public long CustomerId { get; set; }
}