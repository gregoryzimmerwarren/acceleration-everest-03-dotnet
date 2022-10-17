namespace AppModels.Portfolios;

public class PortfolioResultForOthersDtos
{
    protected PortfolioResultForOthersDtos() { }

    public PortfolioResultForOthersDtos(
        long id,
        string name,
        decimal totalBalance)
    {
        Id = id;
        Name = name;
        TotalBalance = totalBalance;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public decimal TotalBalance { get; set; }
}