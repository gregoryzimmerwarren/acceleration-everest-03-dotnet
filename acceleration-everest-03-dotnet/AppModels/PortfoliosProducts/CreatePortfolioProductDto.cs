namespace AppModels.PortfoliosProducts;

public class CreatePortfolioProductDto
{
    public CreatePortfolioProductDto(
        long portfolioId, 
        long productId)
    {
        PortfolioId = portfolioId;
        ProductId = productId;
    }

    public long PortfolioId { get; set; }
    public long ProductId { get; set; }
}