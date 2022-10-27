using AppModels.Portfolios;
using Bogus;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Portfolios;

public class PortfolioResultForOthersDtosFixture
{
    public static PortfolioResultForOthersDtos GeneratePortfolioResultForOthersDtosBogusFixture()
    {
        var testPortfolioResultForOthersDtosBogus = new Faker<PortfolioResultForOthersDtos>("pt_BR")
            .CustomInstantiator(faker => new PortfolioResultForOthersDtos(
                id: faker.Random.Long(0, 10),
                name: faker.Random.String2(5),
                totalBalance: faker.Random.Decimal()));

        var portfolioResultForOthersDtosBogus = testPortfolioResultForOthersDtosBogus.Generate();
        return portfolioResultForOthersDtosBogus;
    }
    
    public static IEnumerable<PortfolioResultForOthersDtos> GenerateListPortfolioResultForOthersDtosBogusFixture(int generatedQuantity)
    {
        var testListPortfolioResultForOthersDtosBogus = new Faker<PortfolioResultForOthersDtos>("pt_BR")
            .CustomInstantiator(faker => new PortfolioResultForOthersDtos(
                id: faker.Random.Long(0, 10),
                name: faker.Random.String2(5),
                totalBalance: faker.Random.Decimal()));

        var listPortfolioResultForOthersDtosBogus = testListPortfolioResultForOthersDtosBogus.Generate(generatedQuantity);
        return listPortfolioResultForOthersDtosBogus;
    }
}