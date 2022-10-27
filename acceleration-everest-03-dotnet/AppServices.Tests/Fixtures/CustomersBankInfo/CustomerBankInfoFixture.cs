using Bogus;
using DomainModels.Models;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.CustomersBankInfo;

public class CustomerBankInfoFixture
{
    public static CustomerBankInfo GenerateCustomerBankInfoFixture()
    {
        var testCustomerBankInfoDto = new Faker<CustomerBankInfo>("pt_BR")
            .CustomInstantiator(faker => new CustomerBankInfo(
                customerId: 1,
                accountBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m)));

        var customerBankInfoDto = testCustomerBankInfoDto.Generate();
        return customerBankInfoDto;
    }

    public static IEnumerable<CustomerBankInfo> GenerateListCustomerBankInfoFixture(int generatedQuantity)
    {
        var testListCustomerBankInfoDto = new Faker<CustomerBankInfo>("pt_BR")
            .CustomInstantiator(faker => new CustomerBankInfo(
                customerId: 1,
                accountBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m)));

        var listCustomerBankInfoDto = testListCustomerBankInfoDto.Generate(generatedQuantity);
        return listCustomerBankInfoDto;
    }
}