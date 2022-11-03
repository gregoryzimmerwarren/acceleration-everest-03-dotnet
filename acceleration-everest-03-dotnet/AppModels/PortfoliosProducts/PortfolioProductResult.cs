using AppModels.Portfolios;
using AppModels.Products;

namespace AppModels.PortfoliosProducts;

public class PortfolioProductResult
{
    protected PortfolioProductResult() { }

    public PortfolioProductResult(
        long id,
        PortfolioResultForOthersDtos portfolio,
        ProductResult product)
    {
        Id = id;
        Portfolio = portfolio;
        Product = product;
    }

    public long Id { get; set; }
    public PortfolioResultForOthersDtos Portfolio { get; set; }
    public ProductResult Product { get; set; }
}