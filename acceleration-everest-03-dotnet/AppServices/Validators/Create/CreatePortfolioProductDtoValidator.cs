using AppModels.PortfoliosProducts;
using FluentValidation;

namespace AppServices.Validators.Create
{
    public class CreatePortfolioProductDtoValidator : AbstractValidator<CreatePortfolioProductDto>
    {
        public CreatePortfolioProductDtoValidator()
        {
            RuleFor(portfolioProduct => portfolioProduct.PortfolioId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(portfolioProduct => portfolioProduct.ProductId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}