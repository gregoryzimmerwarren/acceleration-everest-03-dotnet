using AppModels.Orders;
using DomainModels.Enums;
using FluentValidation;
using System;

namespace AppServices.Validators.Create;

public class CreateOrderValidator : AbstractValidator<CreateOrder>
{
    public CreateOrderValidator()
    {
        RuleFor(order => order.Quotes)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(order => order.LiquidatedAt)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateTime.Now.Date);

        RuleFor(order => order.Direction)
            .NotNull()
            .IsInEnum();

        RuleFor(order => order.PortfolioId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(order => order.ProductId)
            .NotEmpty()
            .GreaterThan(0);
    }
}