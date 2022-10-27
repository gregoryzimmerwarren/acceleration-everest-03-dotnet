using AppModels.Products;
using Bogus;
using DomainModels.Enums;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Products;

public class ProductResultForOthersDtosFixture
{
    public static ProductResultForOthersDtos GenerateProductResultForOthersDtosFixture()
    {
        var testProductResultForOthersDtos = new Faker<ProductResultForOthersDtos>("pt_BR")
            .CustomInstantiator(faker => new ProductResultForOthersDtos(
                id: faker.Random.Long(),
                symbol: faker.Random.String2(5),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                daysToExpire: faker.Random.Int(0),
                expirationAt: faker.Date.Future(1),
                type: faker.PickRandom<ProductType>()));

        var productResultForOthersDtos = testProductResultForOthersDtos.Generate();
        return productResultForOthersDtos;
    }

    public static IEnumerable<ProductResultForOthersDtos> GenerateListProductResultForOthersDtosFixture(int generatedQuantity)
    {
        var testListProductResultForOthersDtos = new Faker<ProductResultForOthersDtos>("pt_BR")
            .CustomInstantiator(faker => new ProductResultForOthersDtos(
                id: faker.Random.Long(),
                symbol: faker.Random.String2(5),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                daysToExpire: faker.Random.Int(0),
                expirationAt: faker.Date.Future(1),
                type: faker.PickRandom<ProductType>()));

        var listProductResultForOthersDtos = testListProductResultForOthersDtos.Generate(generatedQuantity);
        return listProductResultForOthersDtos;
    }
}