using Bogus;
using DomainModels.Enums;
using DomainModels.Models;
using System;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Orders;

public class OrderFixture
{
    public static Order GenerateOrderFixture()
    {
        var testOrderDto = new Faker<Order>("pt_BR")
            .CustomInstantiator(faker => new Order(
                quotes: faker.Random.Int(min: 1, max: 10),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                liquidatedAt: DateTime.Now,
                direction: faker.PickRandom<OrderDirection>(),
                wasExecuted: faker.Random.Bool(),
                portfolioId: 1,
                productId: 1));

        var orderDto = testOrderDto.Generate();
        return orderDto;
    }

    public static IEnumerable<Order> GenerateListOrderFixture(int generatedQuantity)
    {
        var testListOrderDto = new Faker<Order>("pt_BR")
            .CustomInstantiator(faker => new Order(
                quotes: faker.Random.Int(min: 1, max: 10),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                liquidatedAt: DateTime.Now,
                direction: faker.PickRandom<OrderDirection>(),
                wasExecuted: faker.Random.Bool(),
                portfolioId: 1,
                productId: 1));

        var listOrderDto = testListOrderDto.Generate(generatedQuantity);
        return listOrderDto;
    }
}