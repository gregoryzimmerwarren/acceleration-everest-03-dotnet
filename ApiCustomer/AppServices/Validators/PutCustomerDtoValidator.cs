using AppModels.DTOs;
using FluentValidation;

namespace AppServices.Validators
{
    internal class PutCustomerDtoValidator : AbstractValidator<PostCustomerDto>
    {
        public PutCustomerDtoValidator()
        {
            RuleFor(customer => customer.FullName)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(customer => customer.Email)
                .NotEmpty()
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("Email must have a valid format 'email@email.com'.")
                .Equal(costumer => costumer.EmailConfirmation);            

            RuleFor(costumer => costumer.Cellphone)
                .NotEmpty();

            RuleFor(costumer => costumer.Country)
                .NotEmpty()
                .MinimumLength(4);

            RuleFor(costumer => costumer.City)
                .NotEmpty();

            RuleFor(costumer => costumer.Address)
                .NotEmpty()
                .MinimumLength(4);

            RuleFor(costumer => costumer.PostalCode)
                .NotEmpty();

            RuleFor(costumer => costumer.Number)
                .NotEmpty();
        }
    }
}