using AppModels.CustomersBankInfo;
using AppServices.Tests.Fixtures.Customers;
using Bogus;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.CustomersBankInfo;

public class CustomerBankInfoResultFixture
{
    public static CustomerBankInfoResult GenerateCustomerBankInfoResultFixture()
    {
        var testCustomerBankInfoResult = new Faker<CustomerBankInfoResult>("pt_BR")
            .CustomInstantiator(faker => new CustomerBankInfoResult(
                id: 1,
                accountBalance: faker.Random.Decimal(),
                customer: CustomerResultForOtherDtosFixture.GenerateCustomerResultForOtherDtosFixture()));

        var customerBankInfoResult = testCustomerBankInfoResult.Generate();
        return customerBankInfoResult;
    }

    public static IEnumerable<CustomerBankInfoResult> GenerateListCustomerBankInfoResultFixture(int generatedQuantity)
    {
        var testListCustomerBankInfoResult = new Faker<CustomerBankInfoResult>("pt_BR")
            .CustomInstantiator(faker => new CustomerBankInfoResult(
                id: 1,
                accountBalance: faker.Random.Decimal(),
                customer: CustomerResultForOtherDtosFixture.GenerateCustomerResultForOtherDtosFixture()));

        var listCustomerBankInfoResult = testListCustomerBankInfoResult.Generate(generatedQuantity);
        return listCustomerBankInfoResult;
    }
}
