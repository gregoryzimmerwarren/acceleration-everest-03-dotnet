using Data.Entities;
using FluentValidation;

namespace Data.Validator
{
    public class CustomerValidator : AbstractValidator<CustomerEntity>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.FullName)
                .NotEmpty()
                .WithMessage("Full name must not be empty.")
                .MinimumLength(6)
                .WithMessage("Full name must be at least 6 characters long.");

            RuleFor(customer => customer.Email)
                .NotEmpty()
                .WithMessage("Email must not be empty.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("Email must have a valid format.")
                .Equal(costumer => costumer.EmailConfirmation)
                .WithMessage("Email and EmailConfirmation must be the same.");
               
            RuleFor(costumer => costumer.Cpf)
                .NotEmpty()
                .WithMessage("Cpf must not be empty.")
                .MinimumLength(11)
                .Must(BeValidCpf)
                .WithMessage("Cpf must be valid.");

            RuleFor(costumer => costumer.Cellphone)
                .NotEmpty()
                .WithMessage("Cellphone must not be empty.");

            RuleFor(costumer => costumer.Country)
                .NotEmpty()
                .WithMessage("Country must not be empty.")
                .MinimumLength(4)
                .WithMessage("Country must be at least 4 characters long.");
            
            RuleFor(costumer => costumer.City)
                .NotEmpty()
                .WithMessage("City must not be empty.");
            
            RuleFor(costumer => costumer.Address)
                .NotEmpty()
                .WithMessage("Address must not be empty.")
                .MinimumLength(4)
                .WithMessage("Address must be at least 4 characters long.");
            
            RuleFor(costumer => costumer.PostalCode)
                .NotEmpty()
                .WithMessage("Postal code must not be empty.");

            RuleFor(costumer => costumer.Number)
                .NotEmpty()
                .WithMessage("Number code must not be empty.");

            RuleFor(costumer => costumer.DateOfBirth)
                .NotEmpty()
                .WithMessage("Date of birth code must not be empty.")
                .LessThan(DateTime.Now.Date)
                .WithMessage("Birth date must be older than today.");            
        }

        public bool BeValidCpf(string cpf)
        {           
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
