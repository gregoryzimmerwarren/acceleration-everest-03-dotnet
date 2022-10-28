using AppModels.Orders;
using Bogus;
using DomainModels.Enums;
using System;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Orders;

public class OrderResultOtherDtosFixture
{
    public static OrderResultOtherDtos GenerateOrderResultOtherDtosFixture()
    {
        var testOrderResultOtherDtosDto = new Faker<OrderResultOtherDtos>("pt_BR")
            .CustomInstantiator(faker => new OrderResultOtherDtos(
                id: faker.Random.Long(min: 1, max: 10),
                quotes: faker.Random.Int(min: 1, max: 10),
                netValue: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                liquidatedAt: DateTime.Now,
                direction: faker.PickRandom<OrderDirection>(),
                wasExecuted: false));

        var orderResultOtherDtosDto = testOrderResultOtherDtosDto.Generate();
        return orderResultOtherDtosDto;
    }

    public static IEnumerable<OrderResultOtherDtos> GenerateListOrderResultOtherDtosFixture(int generatedQuantity)
    {
        var testListOrderResultOtherDtosDto = new Faker<OrderResultOtherDtos>("pt_BR")
            .CustomInstantiator(faker => new OrderResultOtherDtos(
                id: faker.Random.Long(min: 1, max: 10),
                quotes: faker.Random.Int(min: 1, max: 10),
                netValue: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                liquidatedAt: DateTime.Now,
                direction: faker.PickRandom<OrderDirection>(),
                wasExecuted: false));

        var listOrderResultOtherDtosDto = testListOrderResultOtherDtosDto.Generate(generatedQuantity);
        return listOrderResultOtherDtosDto;
    }
}