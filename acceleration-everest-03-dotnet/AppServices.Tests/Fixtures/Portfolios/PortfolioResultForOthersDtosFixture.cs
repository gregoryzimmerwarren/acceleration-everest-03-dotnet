using AppModels.Portfolios;
using Bogus;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Portfolios;

public class PortfolioResultForOthersDtosFixture
{
    public static PortfolioResultForOthersDtos GeneratePortfolioResultForOthersDtosFixture()
    {
        var testPortfolioResultForOthersDtos = new Faker<PortfolioResultForOthersDtos>("pt_BR")
            .CustomInstantiator(faker => new PortfolioResultForOthersDtos(
                id: faker.Random.Long(0, 10),
                name: faker.Random.String2(5),
                totalBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m)));

        var portfolioResultForOthersDtos = testPortfolioResultForOthersDtos.Generate();
        return portfolioResultForOthersDtos;
    }
    
    public static IEnumerable<PortfolioResultForOthersDtos> GenerateListPortfolioResultForOthersDtosFixture(int generatedQuantity)
    {
        var testListPortfolioResultForOthersDtos = new Faker<PortfolioResultForOthersDtos>("pt_BR")
            .CustomInstantiator(faker => new PortfolioResultForOthersDtos(
                id: faker.Random.Long(0, 10),
                name: faker.Random.String2(5),
                totalBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m)));

        var listPortfolioResultForOthersDtos = testListPortfolioResultForOthersDtos.Generate(generatedQuantity);
        return listPortfolioResultForOthersDtos;
    }
}