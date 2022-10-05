using AppModels.CustomersBankInfo;
using DomainModels.Models;
using FluentValidation;

namespace AppServices.Validators;

public class CreateCustomerBankInfoDtoValidator : AbstractValidator<CreateCustomerBankInfoDto>
{
    public CreateCustomerBankInfoDtoValidator()
    {
        RuleFor(customerBankInfo => customerBankInfo.CustomerId)
            .NotEmpty()
            .GreaterThan(0);
    }
}