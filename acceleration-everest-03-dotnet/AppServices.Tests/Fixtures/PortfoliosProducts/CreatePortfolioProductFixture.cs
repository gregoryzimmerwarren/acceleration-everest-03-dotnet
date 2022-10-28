using AppModels.PortfoliosProducts;
using Bogus;

namespace AppServices.Tests.Fixtures.PortfoliosProducts;

public class CreatePortfolioProductFixture
{
    public static CreatePortfolioProduct GenerateCreatePortfolioProductFixture()
    {
        var testCreatePortfolioProductDto = new Faker<CreatePortfolioProduct>("pt_BR")
            .CustomInstantiator(faker => new CreatePortfolioProduct(
                portfolioId: 1,
                productId: 1));

        var CreatePortfolioProductDto = testCreatePortfolioProductDto.Generate();
        return CreatePortfolioProductDto;
    }
}