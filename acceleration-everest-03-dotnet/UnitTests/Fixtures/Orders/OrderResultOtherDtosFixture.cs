using AppModels.Enums;
using AppModels.Orders;
using Bogus;
using UnitTests.Fixtures.Products;

namespace UnitTests.Fixtures.Orders;

public class OrderResultOtherDtosFixture
{
     public static IEnumerable<OrderResultForOtherDtos> GenerateListOrderResultOtherDtosFixture(int generatedQuantity)
    {
        var testListOrderResultOtherDtosDto = new Faker<OrderResultForOtherDtos>("pt_BR")
            .CustomInstantiator(faker => new OrderResultForOtherDtos(
                id: faker.Random.Long(min: 1, max: 10),
                quotes: faker.Random.Int(min: 1, max: 10),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                netValue: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                liquidatedAt: DateTime.Now,
                direction: faker.PickRandom<OrderDirection>(),
                wasExecuted: false,
                product: ProductResultFixture.GenerateProductResultFixture()));

        var listOrderResultOtherDtosDto = testListOrderResultOtherDtosDto.Generate(generatedQuantity);
        return listOrderResultOtherDtosDto;
    }
}