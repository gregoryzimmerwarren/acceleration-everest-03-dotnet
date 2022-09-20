using AppModels;
using AppServices.Extensions;
using FluentValidation;
using FluentValidation.Validators;

namespace AppServices.Validators;

public class PutCustomerDtoValidator : AbstractValidator<UpdateCustomerDto>
{
    public PutCustomerDtoValidator()
    {
        RuleFor(customer => customer.FullName)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(customer => customer.Email)
            .NotEmpty()
            .EmailAddress(EmailValidationMode.Net4xRegex)
            .WithMessage("Email must have a valid format 'email@email.com'.");

        RuleFor(customer => customer)
            .Must(customer => customer.EmailConfirmation == customer.Email);

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
            .Must(customer => customer.IsOver18())
            .WithMessage("Customer must be over 18 years old.");
    }
}