using AppModels.Portfolios;
using FluentValidation;

namespace AppServices.Validators.Create;

public class CreatePortfolioValidator : AbstractValidator<CreatePortfolio>
{
    public CreatePortfolioValidator()
    {
        RuleFor(portfolio => portfolio.Name)
            .NotEmpty();

        RuleFor(portfolio => portfolio.Description)
            .NotEmpty()
            .MinimumLength(5);

        RuleFor(portfolio => portfolio.CustomerId)
            .NotEmpty()
            .GreaterThan(0);
    }
}