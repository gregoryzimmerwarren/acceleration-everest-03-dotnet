using AppModels.PortfoliosProducts;
using Bogus;
using System.Collections.Generic;
using UnitTests.Fixtures.Portfolios;
using UnitTests.Fixtures.Products;

namespace UnitTests.Fixtures.PortfoliosProducts;

public class PortfolioProductResultFixture
{
    public static PortfolioProductResult GeneratePortfolioProductResultFixture()
    {
        var testPortfolioProductResultBogus = new Faker<PortfolioProductResult>("pt_BR")
            .CustomInstantiator(faker => new PortfolioProductResult(
                id: faker.Random.Long(0, 10),
                portfolio: PortfolioResultForOthersDtosFixture.GeneratePortfolioResultForOthersDtosFixture(),
                product: ProductResultFixture.GenerateProductResultFixture()));

        var portfolioProductResultBogus = testPortfolioProductResultBogus.Generate();
        return portfolioProductResultBogus;
    }

    public static IEnumerable<PortfolioProductResult> GenerateListPortfolioProductResultFixture(int generatedQuantity)
    {
        var testListPortfolioProductResultBogus = new Faker<PortfolioProductResult>("pt_BR")
            .CustomInstantiator(faker => new PortfolioProductResult(
                id: faker.Random.Long(0, 10),
                portfolio: PortfolioResultForOthersDtosFixture.GeneratePortfolioResultForOthersDtosFixture(),
                product: ProductResultFixture.GenerateProductResultFixture()));

        var listPortfolioProductResultBogus = testListPortfolioProductResultBogus.Generate(generatedQuantity);
        return listPortfolioProductResultBogus;
    }
}