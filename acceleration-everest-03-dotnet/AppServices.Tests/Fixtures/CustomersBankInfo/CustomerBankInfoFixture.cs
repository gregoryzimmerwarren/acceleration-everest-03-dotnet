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
                accountBalance: faker.Random.Decimal()));

        var customerBankInfoDto = testCustomerBankInfoDto.Generate();
        return customerBankInfoDto;
    }

    public static IEnumerable<CustomerBankInfo> GenerateListCustomerBankInfoFixture(int generatedQuantity)
    {
        var testListCustomerBankInfoDto = new Faker<CustomerBankInfo>("pt_BR")
            .CustomInstantiator(faker => new CustomerBankInfo(
                customerId: 1,
                accountBalance: faker.Random.Decimal()));

        var listCustomerBankInfoDto = testListCustomerBankInfoDto.Generate(generatedQuantity);
        return listCustomerBankInfoDto;
    }
}