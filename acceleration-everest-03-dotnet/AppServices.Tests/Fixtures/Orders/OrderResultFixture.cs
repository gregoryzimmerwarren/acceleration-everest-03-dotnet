using AppModels.Orders;
using AppServices.Tests.Fixtures.Portfolios;
using AppServices.Tests.Fixtures.Products;
using Bogus;
using DomainModels.Enums;
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
                netValue: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                liquidatedAt: DateTime.Now,
                direction: faker.PickRandom<OrderDirection>(),
                wasExecuted: false,
                portfolio: PortfolioResultForOthersDtosFixture.GeneratePortfolioResultForOthersDtosFixture(),
                product: ProductResultForOthersDtosFixture.GenerateProductResultForOthersDtosFixture()));

        var orderResultDto = testOrderResultDto.Generate();
        return orderResultDto;
    }

    public static IEnumerable<OrderResult> GenerateListOrderResultFixture(int generatedQuantity)
    {
        var testListOrderResultDto = new Faker<OrderResult>("pt_BR")
            .CustomInstantiator(faker => new OrderResult(
                id: faker.Random.Long(min: 1, max: 10),
                quotes: faker.Random.Int(min: 1, max: 10),
                netValue: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                liquidatedAt: DateTime.Now,
                direction: faker.PickRandom<OrderDirection>(),
                wasExecuted: false,
                portfolio: PortfolioResultForOthersDtosFixture.GeneratePortfolioResultForOthersDtosFixture(),
                product: ProductResultForOthersDtosFixture.GenerateProductResultForOthersDtosFixture()));

        var listOrderResultDto = testListOrderResultDto.Generate(generatedQuantity);
        return listOrderResultDto;
    }
}