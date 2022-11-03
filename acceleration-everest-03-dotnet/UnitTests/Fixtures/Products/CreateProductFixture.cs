using AppModels.Products;
using Bogus;
using Infrastructure.CrossCutting.Enums;
using System;

namespace UnitTests.Fixtures.Products;

public class CreateProductFixture
{
    public static CreateProduct GenerateCreateProductFixture()
    {
        var testCreateProductDto = new Faker<CreateProduct>("pt_BR")
            .CustomInstantiator(faker => new CreateProduct(
                symbol: faker.Random.String2(5),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                issuanceAt: DateTime.Now,
                expirationAt: DateTime.Now,
                type: (int)faker.PickRandom<ProductType>()));

        var createProductDto = testCreateProductDto.Generate();
        return createProductDto;
    }
}