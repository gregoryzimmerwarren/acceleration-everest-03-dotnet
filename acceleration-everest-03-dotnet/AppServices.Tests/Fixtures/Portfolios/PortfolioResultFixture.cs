using AppModels.Portfolios;
using AppServices.Tests.Fixtures.Customers;
using AppServices.Tests.Fixtures.Orders;
using AppServices.Tests.Fixtures.Products;
using Bogus;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Portfolios;

public class PortfolioResultFixture
{
    public static PortfolioResult GeneratePortfolioResultFixture()
    {
        var testPortfolioResultBogus = new Faker<PortfolioResult>("pt_BR")
            .CustomInstantiator(faker => new PortfolioResult(
                id: faker.Random.Long(0, 10),
                name: faker.Random.String2(5),
                description: faker.Lorem.Random.Words(10),
                totalBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                accountBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                customer: CustomerResultForOtherDtosFixture.GenerateCustomerResultForOtherDtosFixture(),
                products: ProductResultForOthersDtosFixture.GenerateListProductResultForOthersDtosFixture(3),
                orders: OrderResultOtherDtosFixture.GenerateListOrderResultOtherDtosFixture(3)));

        var portfolioResultBogus = testPortfolioResultBogus.Generate();
        return portfolioResultBogus;
    }

    public static IEnumerable<PortfolioResult> GenerateListPortfolioResultFixture(int generatedQuantity)
    {
        var testListPortfolioResultBogus = new Faker<PortfolioResult>("pt_BR")
            .CustomInstantiator(faker => new PortfolioResult(
                id: faker.Random.Long(0, 10),
                name: faker.Random.String2(5),
                description: faker.Lorem.Random.Words(10),
                totalBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                accountBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                customer: CustomerResultForOtherDtosFixture.GenerateCustomerResultForOtherDtosFixture(),
                products: ProductResultForOthersDtosFixture.GenerateListProductResultForOthersDtosFixture(3),
                orders: OrderResultOtherDtosFixture.GenerateListOrderResultOtherDtosFixture(3)));

        var listPortfolioResultBogus = testListPortfolioResultBogus.Generate(generatedQuantity);
        return listPortfolioResultBogus;
    }
}