using AppModels;
using AppServices.Extensions;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Linq;

namespace AppServices.Validators;

public class UpdateCustomerDtoValidator : AbstractValidator<UpdateCustomerDto>
{
    public UpdateCustomerDtoValidator()
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
                .Must(IsValidCpf)
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

    private bool IsValidCpf(string cpf)
    {
        cpf = cpf.FormatCpf();

        if (cpf.Length != 11)
            return false;

        if (cpf.All(character => character == cpf.First()))
            return false;

        int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string digit;
        int sum;
        int rest;

        sum = 0;

        for (int i = 0; i < 9; i++)
            sum += Convert.ToInt32(cpf[i].ToString()) * multiplier1[i];

        rest = sum % 11;

        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit = rest.ToString();

        sum = 0;

        for (int i = 0; i < 10; i++)
            sum += Convert.ToInt32(cpf[i].ToString()) * multiplier2[i];

        rest = sum % 11;

        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit = digit + rest.ToString();

        return cpf.EndsWith(digit);
    }
}