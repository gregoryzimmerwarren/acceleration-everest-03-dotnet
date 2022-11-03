using AppModels.Portfolios;
using Bogus;

namespace UnitTests.Fixtures.Portfolios;

public class CreatePortfolioFixture
{
    public static CreatePortfolio GenerateCreatePortfolioFixture()
    {
        var testCreatePortfolioDto = new Faker<CreatePortfolio>("pt_BR")
            .CustomInstantiator(faker => new CreatePortfolio(
                name: faker.Random.String2(5),
                description: faker.Lorem.Random.Words(10),
                customerId: 1));

        var CreatePortfolioDto = testCreatePortfolioDto.Generate();
        return CreatePortfolioDto;
    }
}