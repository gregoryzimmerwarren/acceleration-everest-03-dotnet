using Bogus;
using DomainModels.Models;

namespace UnitTests.Fixtures.PortfoliosProducts;

public class PortfolioProductFixture
{
    public static PortfolioProduct GeneratePortfolioProductFixture()
    {
        var testPortfolioProduct = new Faker<PortfolioProduct>("pt_BR")
            .CustomInstantiator(faker => new PortfolioProduct(
                portfolioId: 1,
                productId: 1))
                .RuleFor(portfolioProduct => portfolioProduct.Id, 1);

        var portfolioProduct = testPortfolioProduct.Generate();
        return portfolioProduct;
    }
}