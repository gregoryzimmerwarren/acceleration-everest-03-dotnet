using AppModels.CustomersBankInfo;
using Bogus;
using System.Collections.Generic;

namespace UnitTests.Fixtures.CustomersBankInfo;

public class CustomerBankInfoResultForCustomerDtosFixture
{
    public static CustomerBankInfoResultForCustomerDtos GenerateCustomerBankInfoResultForCustomerDtosFixture()
    {
        var testCustomerBankInfoResultForCustomerDtos = new Faker<CustomerBankInfoResultForCustomerDtos>("pt_BR")
            .CustomInstantiator(faker => new CustomerBankInfoResultForCustomerDtos(
                id: 1,
                accountBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m)));

        var customerBankInfoResultForCustomerDtos = testCustomerBankInfoResultForCustomerDtos.Generate();
        return customerBankInfoResultForCustomerDtos;
    }

    public static IEnumerable<CustomerBankInfoResultForCustomerDtos> GenerateListCustomerBankInfoResultForCustomerDtosFixture(int generatedQuantity)
    {
        var testListCustomerBankInfoResultForCustomerDtos = new Faker<CustomerBankInfoResultForCustomerDtos>("pt_BR")
            .CustomInstantiator(faker => new CustomerBankInfoResultForCustomerDtos(
                id: 1,
                accountBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m)));

        var listCustomerBankInfoResultForCustomerDtos = testListCustomerBankInfoResultForCustomerDtos.Generate(generatedQuantity);
        return listCustomerBankInfoResultForCustomerDtos;
    }
}