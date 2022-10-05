namespace AppModels.Portfolios;

public class CreatePortfolioDto
{
    public CreatePortfolioDto(
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

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal TotalBalance { get; set; }
    public decimal AccountBalance { get; set; }
    public long CustomerId { get; set; }
}