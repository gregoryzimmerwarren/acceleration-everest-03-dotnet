using AppModels.PortfoliosProducts;
using AppServices.Tests.Fixtures.Portfolios;
using AppServices.Tests.Fixtures.Products;
using Bogus;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.PortfoliosProducts;

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