using AppModels.PortfoliosProducts;
using FluentValidation;

namespace AppServices.Validators.Create
{
    public class CreatePortfolioProductValidator : AbstractValidator<CreatePortfolioProduct>
    {
        public CreatePortfolioProductValidator()
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