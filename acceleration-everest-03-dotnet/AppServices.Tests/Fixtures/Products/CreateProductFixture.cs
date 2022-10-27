using AppModels.Products;
using Bogus;
using DomainModels.Enums;

namespace AppServices.Tests.Fixtures.Products;

public class CreateProductFixture
{
    public static CreateProduct GenerateCreateProductFixture()
    {
        var testCreateProductDto = new Faker<CreateProduct>("pt_BR")
            .CustomInstantiator(faker => new CreateProduct(
                symbol: faker.Random.String2(5),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                issuanceAt: faker.Date.Future(1),
                expirationAt: faker.Date.Future(1),
                type: (int)faker.PickRandom<ProductType>()));

        var createProductDto = testCreateProductDto.Generate();
        return createProductDto;
    }
}