using AppModels.Enums;
using AppModels.Products;
using AppServices.Tests.Fixtures.Orders;
using AppServices.Tests.Fixtures.Portfolios;
using Bogus;
using System;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Products;

public class ProductResultFixture
{
    public static ProductResult GenerateProductResultFixture()
    {
        var testProductResult = new Faker<ProductResult>("pt_BR")
            .CustomInstantiator(faker => new ProductResult(
                id: faker.Random.Long(min: 1, max: 10),
                symbol: faker.Random.String2(5),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                daysToExpire: faker.Random.Int(0),
                expirationAt: DateTime.Now,
                type: faker.PickRandom<ProductType>()));

        var productResult = testProductResult.Generate();
        return productResult;
    }

    public static IEnumerable<ProductResult> GenerateListProductResultFixture(int generatedQuantity)
    {
        var testListProductResult = new Faker<ProductResult>("pt_BR")
            .CustomInstantiator(faker => new ProductResult(
                id: faker.Random.Long(min: 1, max: 10),
                symbol: faker.Random.String2(5),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                daysToExpire: faker.Random.Int(0),
                expirationAt: DateTime.Now,
                type: faker.PickRandom<ProductType>()));

        var listProductResult = testListProductResult.Generate(generatedQuantity);
        return listProductResult;
    }
}
