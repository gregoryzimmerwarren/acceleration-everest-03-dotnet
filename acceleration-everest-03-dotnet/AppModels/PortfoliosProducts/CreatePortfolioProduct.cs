namespace AppModels.PortfoliosProducts;

public class CreatePortfolioProduct
{
    public CreatePortfolioProduct(
        long portfolioId, 
        long productId)
    {
        PortfolioId = portfolioId;
        ProductId = productId;
    }

    public long PortfolioId { get; set; }
    public long ProductId { get; set; }
}