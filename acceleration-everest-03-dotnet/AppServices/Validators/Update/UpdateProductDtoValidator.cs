using AppModels.Products;
using FluentValidation;
using System;

namespace AppServices.Validators.Update
{
    internal class UpdateProductDtoValidator : AbstractValidator<CreateProduct>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(product => product.Symbol)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(product => product.UnitPrice)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Unit price must be more than R$0,00.");

            RuleFor(product => product.IssuanceAt)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Now.Date);

            RuleFor(product => product.ExpirationAt)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Now.Date);
        }
    }
}