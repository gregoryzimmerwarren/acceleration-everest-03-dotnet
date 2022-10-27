using AppModels.Customers;
using AppModels.Orders;
using Bogus;

namespace AppServices.Tests.Fixtures.Orders;

public class CreateOrderFixture
{
    public static CreateOrder GenerateCreateOrderFixture()
    {
        var testCreateOrderDto = new Faker<CreateOrder>("pt_BR")
            .CustomInstantiator(faker => new CreateOrder(
                quotes: faker.Random.Int(),
                liquidatedAt: faker.Date.Future(1),
                portfolioId: 1,
                productId: 1));

        var createOrderDto = testCreateOrderDto.Generate();
        return createOrderDto;
    }
}