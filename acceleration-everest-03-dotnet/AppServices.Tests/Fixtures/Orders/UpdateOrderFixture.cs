using AppModels.Orders;
using Bogus;
using DomainModels.Enums;

namespace AppServices.Tests.Fixtures.Orders;

public class UpdateOrderFixture
{
    public static UpdateOrder GenerateUpdateOrderFixture()
    {
        var testUpdateOrderDto = new Faker<UpdateOrder>("pt_BR")
            .CustomInstantiator(faker => new UpdateOrder(
                id: faker.Random.Long(),
                quotes: faker.Random.Int(),
                netValue: faker.Random.Int(),
                direction: faker.PickRandom<OrderDirection>().ToString(),
                wasExecuted: true,
                liquidatedAt: faker.Date.Future(1),
                portfolioId: 1,
                productId: 1));

        var updateOrderDto = testUpdateOrderDto.Generate();
        return updateOrderDto;
    }
}