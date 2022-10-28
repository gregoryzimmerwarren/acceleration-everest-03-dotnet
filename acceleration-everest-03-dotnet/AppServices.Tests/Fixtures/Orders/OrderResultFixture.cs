﻿using AppModels.Enums;
using AppModels.Orders;
using AppServices.Tests.Fixtures.Portfolios;
using AppServices.Tests.Fixtures.Products;
using Bogus;
using System;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Orders;

public class OrderResultFixture
{
    public static OrderResult GenerateOrderResultFixture()
    {
        var testOrderResultDto = new Faker<OrderResult>("pt_BR")
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

        var orderResultDto = testOrderResultDto.Generate();
        return orderResultDto;
    }

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