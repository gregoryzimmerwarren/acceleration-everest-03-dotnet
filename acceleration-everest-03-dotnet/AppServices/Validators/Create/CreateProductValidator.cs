using AppModels.Products;
using FluentValidation;
using System;

namespace AppServices.Validators.Create
{
    public class CreateProductValidator : AbstractValidator<CreateProduct>
    {
        public CreateProductValidator()
        {
            RuleFor(product => product.Symbol)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(product => product.UnitPrice)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Unit price must be more than R$0,00.");

            RuleFor(product => product.IssuanceAt)
                .NotEmpty();
            
            RuleFor(product => product.ExpirationAt)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Now.Date);

            RuleFor(product => product.Type)
                .NotEmpty()
                .Must(IsValidEnum);
        }

        private bool IsValidEnum(int direction)
        {
            if (direction < 1 || direction > 5)
                return false;

            return true;
        }
    }
}