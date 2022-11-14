using AppModels.Products;
using Bogus;
using Infrastructure.CrossCutting.Enums;

namespace UnitTests.Fixtures.Products;

public class UpdateProductFixture
{
    public static UpdateProduct GenerateUpdateProductFixture()
    {
        var testUpdateProductDto = new Faker<UpdateProduct>("pt_BR")
            .CustomInstantiator(faker => new UpdateProduct(
                symbol: faker.Random.String2(5),
                unitPrice: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                issuanceAt: DateTime.Now,
                expirationAt: DateTime.Now,
                type: (int)faker.PickRandom<ProductType>()));

        var ipdateProductDto = testUpdateProductDto.Generate();
        return ipdateProductDto;
    }
}