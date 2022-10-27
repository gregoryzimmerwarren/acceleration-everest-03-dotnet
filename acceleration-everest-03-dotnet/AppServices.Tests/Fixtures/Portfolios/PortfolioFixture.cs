using Bogus;
using DomainModels.Models;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Portfolios;

public class PortfolioFixture
{
    public static Portfolio GeneratePortfolioFixture()
    {
        var testPortfolio = new Faker<Portfolio>("pt_BR")
            .CustomInstantiator(faker => new Portfolio(
                name: faker.Random.String(10),
                description: faker.Lorem.Random.Words(10),
                totalBalance: faker.Random.Decimal(),
                accountBalance: faker.Random.Decimal(),
                customerId: 1));

        var portfolio = testPortfolio.Generate();
        return portfolio;
    }

    public static IEnumerable<Portfolio> GenerateListPortfolioFixture(int generatedQuantity)
    {
        var testPortfolio = new Faker<Portfolio>("pt_BR")
            .CustomInstantiator(faker => new Portfolio(
                name: faker.Random.String(10),
                description: faker.Lorem.Random.Words(10),
                totalBalance: faker.Random.Decimal(),
                accountBalance: faker.Random.Decimal(),
                customerId: 1));

        var portfolio = testPortfolio.Generate(generatedQuantity);
        return portfolio;
    }
}