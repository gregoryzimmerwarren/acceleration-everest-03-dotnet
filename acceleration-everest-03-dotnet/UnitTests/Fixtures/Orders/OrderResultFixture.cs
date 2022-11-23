using AppModels.Enums;
using AppModels.Orders;
using Bogus;
using UnitTests.Fixtures.Portfolios;
using UnitTests.Fixtures.Products;

namespace UnitTests.Fixtures.Orders;

public class OrderResultFixture
{
     public static IEnumerable<OrderResult> GenerateListOrderResultFixture(int generatedQuantity)
    {
        var testListOrderResultDto = new Faker<OrderResult>("pt_BR")
            .CustomInstantiator(faker => new OrderResult(
                id: faker.Random.Long(min: 1, max: 10),
                quotes: faker.Random.Int(min: 1, max: 10),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                netValue: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                liquidatedAt: DateTime.Now,
                direction: faker.PickRandom<OrderDirection>(),
                wasExecuted: false,
                portfolio: PortfolioResultForOthersDtosFixture.GeneratePortfolioResultForOthersDtosFixture(),
                product: ProductResultFixture.GenerateProductResultFixture()));

        var listOrderResultDto = testListOrderResultDto.Generate(generatedQuantity);
        return listOrderResultDto;
    }
}