using Bogus;
using DomainModels.Models;

namespace DomainServices.Tests.Fixtures;

public class CustomerBankInfoFixture
{
    public static CustomerBankInfo GenerateCustomerBankInfoFixture()
    {
        var testCustomerBankInfoDto = new Faker<CustomerBankInfo>("pt_BR")
            .CustomInstantiator(faker => new CustomerBankInfo(
                customerId: 1,
                accountBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m)))
                .RuleFor(customerBankInfo => customerBankInfo.Id, 1)
                .RuleFor(customerBankInfo => customerBankInfo.Customer, CustomerFixture.GenerateCustomerFixture());

        var customerBankInfoDto = testCustomerBankInfoDto.Generate();
        return customerBankInfoDto;
    }

    public static IList<CustomerBankInfo> GenerateListCustomerBankInfoFixture(int generatedQuantity)
    {
        var testListCustomerBankInfoDto = new Faker<CustomerBankInfo>("pt_BR")
            .CustomInstantiator(faker => new CustomerBankInfo(
                customerId: 1,
                accountBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m)))
                .RuleFor(customerBankInfo => customerBankInfo.Id, 1)
                .RuleFor(customerBankInfo => customerBankInfo.Customer, CustomerFixture.GenerateCustomerFixture());

        var listCustomerBankInfoDto = testListCustomerBankInfoDto.Generate(generatedQuantity);
        return listCustomerBankInfoDto;
    }
}