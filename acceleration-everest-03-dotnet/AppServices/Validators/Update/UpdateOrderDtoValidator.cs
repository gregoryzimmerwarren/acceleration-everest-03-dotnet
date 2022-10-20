using AppModels.Orders;
using FluentValidation;
using System;

namespace AppServices.Validators.Update
{
    public class UpdateOrderDtoValidator : AbstractValidator<UpdateOrder>
    {
        public UpdateOrderDtoValidator()
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
}