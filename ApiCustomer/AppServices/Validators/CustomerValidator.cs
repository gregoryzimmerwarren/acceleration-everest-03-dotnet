using DomainModels.Models;
using FluentValidation;

namespace AppServices.Validator
{
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.FullName)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(customer => customer.Email)
                .NotEmpty()
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("Email must have a valid format 'email@email.com'.")
                .Equal(costumer => costumer.EmailConfirmation);
               
            RuleFor(costumer => costumer.Cpf)
                .NotEmpty()
                .MinimumLength(11)
                .Must(BeValidCpf)
                .WithMessage("Cpf must be valid.");

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

            RuleFor(costumer => costumer.DateOfBirth)
                .NotEmpty()
                .LessThan(DateTime.Now.Date);
        }

        public bool BeValidCpf(string cpf)
        {
            cpf = cpf.CpfFormatter();

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
}
