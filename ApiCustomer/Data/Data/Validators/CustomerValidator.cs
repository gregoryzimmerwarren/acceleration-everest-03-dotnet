using Data.Entities;
using FluentValidation;

namespace Data.Validator
{
    public class CustomerValidator : AbstractValidator<CustomerEntity>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.FullName)
                .NotNull()
                .WithMessage("Full name must not be null.")
                .NotEmpty()
                .WithMessage("Full name must not be empty.")
                .MinimumLength(6)
                .WithMessage("Full name must be at least 6 characters long.");

            RuleFor(customer => customer.Email)
                .NotNull()
                .WithMessage("Email must not be null.")
                .NotEmpty()
                .WithMessage("Email must not be empty.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("Email must have a valid format.");

            RuleFor(costumer => costumer.EmailConfirmation)
                .Equal(costumer => costumer.Email)
                .WithMessage("Email and EmailConfirmation must be the same.");

            RuleFor(costumer => costumer.Cpf)
                .NotNull()
                .WithMessage("Cpf must not be null.")
                .NotEmpty()
                .WithMessage("Cpf must not be empty.")
                .MinimumLength(11)
                .WithMessage("Cpf must be at least 11 characters long.")
                .MaximumLength(14)
                .WithMessage("Cpf must not be more than 14 characters long.")
                .Must(BeValidCpf)
                .WithMessage("Cpf must be valid.");

            RuleFor(costumer => costumer.Cellphone)
                .NotNull()
                .WithMessage("Cellphone must not be null.")
                .NotEmpty()
                .WithMessage("Cellphone must not be empty.")
                .Matches(@"^\([0-9]{2})\s+9+\([0-9]{8})$")
                .WithMessage("Cellphone must have a valid format xx 9xxxxxxxx.");

            RuleFor(costumer => costumer.Country)
                .NotNull()
                .WithMessage("Country must not be null.")
                .NotEmpty()
                .WithMessage("Country must not be empty.")
                .MinimumLength(4)
                .WithMessage("Country must be at least 4 characters long.");
            
            RuleFor(costumer => costumer.City)
                .NotNull()
                .WithMessage("City must not be null.")
                .NotEmpty()
                .WithMessage("City must not be empty.");
            
            RuleFor(costumer => costumer.Address)
                .NotNull()
                .WithMessage("Address must not be null.")
                .NotEmpty()
                .WithMessage("Address must not be empty.")
                .MinimumLength(4)
                .WithMessage("Address must be at least 4 characters long.");
            
            RuleFor(costumer => costumer.PostalCode)
                .NotNull()
                .WithMessage("Postal code must not be null.")
                .NotEmpty()
                .WithMessage("Postal code must not be empty.")
                .Matches(@"^\([0-9]{5})+-+\([0-9]{3})$")
                .WithMessage("Postal code must have a valid format xxxxx-xxx.");

            RuleFor(costumer => costumer.Number)
                .NotNull()
                .WithMessage("Number code must not be null.")
                .NotEmpty()
                .WithMessage("Number code must not be empty.");

            RuleFor(costumer => costumer.DateOfBirth)
                .NotNull()
                .WithMessage("Date of birth code must not be null.")
                .NotEmpty()
                .WithMessage("Date of birth code must not be empty.")
                .LessThan(DateTime.Now.Date)
                .WithMessage("Birth date must be older than today.");            
        }

        protected bool BeValidCpf(string cpf)
        {
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string temporaryCpf = cpf.Substring(0, 9);
            string digit;
            int sum;
            int rest;

            sum = 0;

            for (int i = 0; i < 9; i++)
                sum += Convert.ToInt32(temporaryCpf[i].ToString()) * multiplier1[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();

            temporaryCpf = temporaryCpf + digit;

            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += Convert.ToInt32(temporaryCpf[i].ToString()) * multiplier2[i];

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
