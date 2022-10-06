using AppModels.CustomersBankInfo;
using FluentValidation;

namespace AppServices.Validators.Create;

public class CreateCustomerBankInfoDtoValidator : AbstractValidator<CreateCustomerBankInfoDto>
{
    public CreateCustomerBankInfoDtoValidator()
    {
        RuleFor(customerBankInfo => customerBankInfo.CustomerId)
            .NotEmpty()
            .GreaterThan(0);
    }
}