using AppModels.Orders;
using Bogus;

namespace UnitTests.Fixtures.Orders;

public class CreateOrderFixture
{
    public static CreateOrder GenerateCreateOrderFixture()
    {
        var testCreateOrderDto = new Faker<CreateOrder>("pt_BR")
            .CustomInstantiator(faker => new CreateOrder(
                quotes: faker.Random.Int(min: 1, max: 10),
                liquidatedAt: DateTime.Now,
                portfolioId: 1,
                productId: 1));

        var createOrderDto = testCreateOrderDto.Generate();
        return createOrderDto;
    }
}