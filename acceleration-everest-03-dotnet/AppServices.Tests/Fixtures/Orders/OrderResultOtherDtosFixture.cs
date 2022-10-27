using AppModels.Orders;
using Bogus;
using DomainModels.Enums;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Orders;

public class OrderResultOtherDtosFixture
{
    public static OrderResultOtherDtos GenerateOrderResultOtherDtosFixture()
    {
        var testOrderResultOtherDtosDto = new Faker<OrderResultOtherDtos>("pt_BR")
            .CustomInstantiator(faker => new OrderResultOtherDtos(
                id: faker.Random.Long(),
                quotes: faker.Random.Int(),
                netValue: faker.Random.Int(),
                liquidatedAt: faker.Date.Future(1),
                direction: faker.PickRandom<OrderDirection>(),
                wasExecuted: false));

        var orderResultOtherDtosDto = testOrderResultOtherDtosDto.Generate();
        return orderResultOtherDtosDto;
    }

    public static IEnumerable<OrderResultOtherDtos> GenerateListOrderResultOtherDtosFixture(int generatedQuantity)
    {
        var testListOrderResultOtherDtosDto = new Faker<OrderResultOtherDtos>("pt_BR")
            .CustomInstantiator(faker => new OrderResultOtherDtos(
                id: faker.Random.Long(),
                quotes: faker.Random.Int(),
                netValue: faker.Random.Int(),
                liquidatedAt: faker.Date.Future(1),
                direction: faker.PickRandom<OrderDirection>(),
                wasExecuted: false));

        var listOrderResultOtherDtosDto = testListOrderResultOtherDtosDto.Generate(generatedQuantity);
        return listOrderResultOtherDtosDto;
    }
}