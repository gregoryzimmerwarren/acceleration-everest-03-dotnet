using AppModels.CustomersBankInfo;
using Bogus;
using UnitTests.Fixtures.Customers;

namespace UnitTests.Fixtures.CustomersBankInfo;

public class CustomerBankInfoResultFixture
{
     public static IEnumerable<CustomerBankInfoResult> GenerateListCustomerBankInfoResultFixture(int generatedQuantity)
    {
        var testListCustomerBankInfoResult = new Faker<CustomerBankInfoResult>("pt_BR")
            .CustomInstantiator(faker => new CustomerBankInfoResult(
                id: 1,
                accountBalance: faker.Random.Decimal(min: 0.1m, max: 10.0m),
                customer: CustomerResultForOtherDtosFixture.GenerateCustomerResultForOtherDtosFixture()));

        var listCustomerBankInfoResult = testListCustomerBankInfoResult.Generate(generatedQuantity);
        return listCustomerBankInfoResult;
    }
}
