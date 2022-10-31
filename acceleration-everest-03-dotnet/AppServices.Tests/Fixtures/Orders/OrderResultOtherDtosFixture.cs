﻿using AppModels.Orders;
using AppServices.Tests.Fixtures.Products;
using Bogus;
using Infrastructure.CrossCutting.Enums;
using System;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Orders;

public class OrderResultOtherDtosFixture
{
    public static OrderResultForOtherDtos GenerateOrderResultOtherDtosFixture()
    {
        var testOrderResultOtherDtosDto = new Faker<OrderResultForOtherDtos>("pt_BR")
            .CustomInstantiator(faker => new OrderResultForOtherDtos(
                id: faker.Random.Long(min: 1, max: 10),
                quotes: faker.Random.Int(min: 1, max: 10),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                netValue: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                liquidatedAt: DateTime.Now,
                direction: faker.PickRandom<OrderDirection>(),
                wasExecuted: false,
                product: ProductResultFixture.GenerateProductResultFixture()));

        var orderResultOtherDtosDto = testOrderResultOtherDtosDto.Generate();
        return orderResultOtherDtosDto;
    }

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