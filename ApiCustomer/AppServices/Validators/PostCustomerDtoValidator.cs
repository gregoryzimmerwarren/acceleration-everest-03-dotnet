using AppModels;
using DomainModels.Extensions;
using FluentValidation;

namespace AppServices.Validators;

public class PostCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
{
    public PostCustomerDtoValidator()
    {
        RuleFor(customer => customer.FullName)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(customer => customer.Email)
            .NotEmpty()
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage("Email must have a valid format 'email@email.com'.")
            .Equal(customer => customer.EmailConfirmation);

        RuleFor(customer => customer.Cpf)
            .NotEmpty()
            .MinimumLength(11)
            .Must(customer => customer.BeValidCpf())
            .WithMessage("Cpf must be valid.");

        RuleFor(customer => customer.Cellphone)
            .NotEmpty();

        RuleFor(customer => customer.Country)
            .NotEmpty()
            .MinimumLength(4);

        RuleFor(customer => customer.City)
            .NotEmpty();

        RuleFor(customer => customer.Address)
            .NotEmpty()
            .MinimumLength(4);

        RuleFor(customer => customer.PostalCode)
            .NotEmpty();

        RuleFor(customer => customer.Number)
            .NotEmpty();

        RuleFor(customer => customer.DateOfBirth)
            .NotEmpty()
            .Must(customer => customer.BeOver18())
            .WithMessage("Customer must be over 18 years old.");
    }        
}
