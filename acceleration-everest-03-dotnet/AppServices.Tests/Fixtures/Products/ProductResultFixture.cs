using AppModels.Products;
using AppServices.Tests.Fixtures.Orders;
using AppServices.Tests.Fixtures.Portfolios;
using Bogus;
using DomainModels.Enums;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Products;

public class ProductResultFixture
{
    public static ProductResult GenerateProductResultFixture()
    {
        var testProductResult = new Faker<ProductResult>("pt_BR")
            .CustomInstantiator(faker => new ProductResult(
                id: faker.Random.Long(),
                symbol: faker.Random.String2(5),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                daysToExpire: faker.Random.Int(0),
                expirationAt: faker.Date.Future(1),
                type: faker.PickRandom<ProductType>(),
                portfolios: PortfolioResultForOthersDtosFixture.GenerateListPortfolioResultForOthersDtosFixture(3),
                orders: OrderResultOtherDtosFixture.GenerateListOrderResultOtherDtosFixture(3)));

        var productResult = testProductResult.Generate();
        return productResult;
    }

    public static IEnumerable<ProductResult> GenerateListProductResultFixture(int generatedQuantity)
    {
        var testListProductResult = new Faker<ProductResult>("pt_BR")
            .CustomInstantiator(faker => new ProductResult(
                id: faker.Random.Long(),
                symbol: faker.Random.String2(5),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                daysToExpire: faker.Random.Int(0),
                expirationAt: faker.Date.Future(1),
                type: faker.PickRandom<ProductType>(),
                portfolios: PortfolioResultForOthersDtosFixture.GenerateListPortfolioResultForOthersDtosFixture(3),
                orders: OrderResultOtherDtosFixture.GenerateListOrderResultOtherDtosFixture(3)));

        var listProductResult = testListProductResult.Generate(generatedQuantity);
        return listProductResult;
    }
}
