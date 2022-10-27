using AppModels.Products;
using Bogus;
using DomainModels.Enums;
using DomainModels.Models;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Products;

public class ProductFixture
{
    public static Product GenerateProductFixture()
    {
        var testProduct = new Faker<Product>("pt_BR")
            .CustomInstantiator(faker => new Product(                
                symbol: faker.Random.String2(5),
                unitPrice: faker.Random.Decimal(),
                issuanceAt: faker.Date.Future(1),
                expirationAt: faker.Date.Future(1),
                type: faker.PickRandom<ProductType>()));

        var product = testProduct.Generate();
        return product;
    }

    public static IEnumerable<Product> GenerateListProductFixture(int generatedQuantity)
    {
        var testProduct = new Faker<Product>("pt_BR")
            .CustomInstantiator(faker => new Product(
                symbol: faker.Random.String2(5),
                unitPrice: faker.Random.Decimal(),
                issuanceAt: faker.Date.Future(1),
                expirationAt: faker.Date.Future(1),
                type: faker.PickRandom<ProductType>()));

        var product = testProduct.Generate(generatedQuantity);
        return product;
    }
}