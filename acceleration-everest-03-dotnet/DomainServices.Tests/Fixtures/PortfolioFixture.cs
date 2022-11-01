using Bogus;
using DomainModels.Models;

namespace DomainServices.Tests.Fixtures;

public class PortfolioFixture
{
    public static Portfolio GeneratePortfolioFixture()
    {
        var testPortfolio = new Faker<Portfolio>("pt_BR")
            .CustomInstantiator(faker => new Portfolio(
                name: faker.Random.String2(10),
                description: faker.Lorem.Random.Words(10),
                totalBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                accountBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                customerId: 1))
                .RuleFor(portfolio => portfolio.Id, 1);

        var portfolio = testPortfolio.Generate();
        return portfolio;
    }

    public static List<Portfolio> GenerateListPortfolioFixture(int generatedQuantity)
    {
        var testPortfolio = new Faker<Portfolio>("pt_BR")
            .CustomInstantiator(faker => new Portfolio(
                name: faker.Random.String2(10),
                description: faker.Lorem.Random.Words(10),
                totalBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                accountBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                customerId: 1))
                .RuleFor(portfolio => portfolio.Id, 1);

        var portfolio = testPortfolio.Generate(generatedQuantity);
        return portfolio;
    }
}