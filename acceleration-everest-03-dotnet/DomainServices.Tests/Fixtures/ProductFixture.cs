using Bogus;
using DomainModels.Models;
using Infrastructure.CrossCutting.Enums;
using System;
using System.Collections.Generic;

namespace DomainServices.Tests.Fixtures;

public class ProductFixture
{
    public static Product GenerateProductFixture()
    {
        var testProduct = new Faker<Product>("pt_BR")
            .CustomInstantiator(faker => new Product(
                symbol: faker.Random.String2(5),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                issuanceAt: DateTime.Now,
                expirationAt: DateTime.Now,
                type: faker.PickRandom<ProductType>()))
                .RuleFor(product => product.Id, 1);

        var product = testProduct.Generate();
        return product;
    }

    public static IEnumerable<Product> GenerateListProductFixture(int generatedQuantity)
    {
        var testProduct = new Faker<Product>("pt_BR")
            .CustomInstantiator(faker => new Product(
                symbol: faker.Random.String2(5),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                issuanceAt: DateTime.Now,
                expirationAt: DateTime.Now,
                type: faker.PickRandom<ProductType>()))
                .RuleFor(product => product.Id, 1);

        var product = testProduct.Generate(generatedQuantity);
        return product;
    }
}