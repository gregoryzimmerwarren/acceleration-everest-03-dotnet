using AppModels.Portfolios;
using AppModels.Products;

namespace AppModels.PortfoliosProducts;

public class PortfolioProductResult
{
    protected PortfolioProductResult() { }

    public PortfolioProductResult(
        long id,
        PortfolioResultForOthersDtos portfolio,
        ProductResultForOthersDtos product)
    {
        Id = id;
        Portfolio = portfolio;
        Product = product;
    }

    public long Id { get; set; }
    public PortfolioResultForOthersDtos Portfolio { get; set; }
    public ProductResultForOthersDtos Product { get; set; }
}