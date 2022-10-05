using AppModels.Orders;
using FluentValidation;
using Infrastructure.CrossCutting.Extensions;

namespace AppServices.Validators;

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(order => order.Quotes)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(order => order.NetValue)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Net value must be more than R$0,00.");

        RuleFor(order => order.LiquidatedAt)
            .NotEmpty()
            .Must(order => order.IsTodayOrLater())
            .WithMessage("Order must be liquidated today or later");

        //RuleFor(order => order.Direction)
        //    .NotNull()
        //    .InclusiveBetween(1, 2);

        RuleFor(order => order.PortifolioId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(order => order.ProductId)
            .NotEmpty()
            .GreaterThan(0);
    }
}