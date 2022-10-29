﻿using AppModels.Orders;
using Bogus;
using DomainModels.Enums;
using System;

namespace AppServices.Tests.Fixtures.Orders;

public class UpdateOrderFixture
{
    public static UpdateOrder GenerateUpdateOrderFixture()
    {
        var testUpdateOrderDto = new Faker<UpdateOrder>("pt_BR")
            .CustomInstantiator(faker => new UpdateOrder(
                id: faker.Random.Long(min: 1, max: 10),
                quotes: faker.Random.Int(min: 1, max: 10),
                unitPrice: faker.Random.Int(min: 1, max: 10),
                direction: faker.PickRandom<OrderDirection>().ToString(),
                wasExecuted: true,
                liquidatedAt: DateTime.Now,
                portfolioId: 1,
                productId: 1));

        var updateOrderDto = testUpdateOrderDto.Generate();
        return updateOrderDto;
    }
}