using AppModels.Customers;
using Bogus;
using Bogus.Extensions.Brazil;
using System.Collections.Generic;
using UnitTests.Fixtures.CustomersBankInfo;
using UnitTests.Fixtures.Portfolios;

namespace UnitTests.Fixtures.Customers;

public class CustomerResultFixture
{
    public static CustomerResult GenerateCustomerResultFixture()
    {
        var testCustomerResult = new Faker<CustomerResult>("pt_BR")
            .CustomInstantiator(faker => new CustomerResult(
                id: faker.Random.Long(0, 10),
                fullName: faker.Name.FirstName() + " " + faker.Name.LastName(),
                email: faker.Internet.Email(),
                cpf: faker.Person.Cpf(),
                cellphone: faker.Phone.PhoneNumberFormat(),
                city: faker.Address.City(),
                postalCode: faker.Address.ZipCode(),
                customerBankInfo: CustomerBankInfoResultForCustomerDtosFixture.GenerateCustomerBankInfoResultForCustomerDtosFixture(),
                portfolios: PortfolioResultForOthersDtosFixture.GenerateListPortfolioResultForOthersDtosFixture(2)));

        var customerResult = testCustomerResult.Generate();
        return customerResult;
    }

    public static IEnumerable<CustomerResult> GenerateListCustomerResultFixture(int generatedQuantity)
    {
        var testListCustomerResult = new Faker<CustomerResult>("pt_BR")
            .CustomInstantiator(faker => new CustomerResult(
                id: faker.Random.Long(0, 10),
                fullName: faker.Name.FirstName() + " " + faker.Name.LastName(),
                email: faker.Internet.Email(),
                cpf: faker.Person.Cpf(),
                cellphone: faker.Phone.PhoneNumberFormat(),
                city: faker.Address.City(),
                postalCode: faker.Address.ZipCode(),
                customerBankInfo: CustomerBankInfoResultForCustomerDtosFixture.GenerateCustomerBankInfoResultForCustomerDtosFixture(),
                portfolios: PortfolioResultForOthersDtosFixture.GenerateListPortfolioResultForOthersDtosFixture(2)));

        var listCustomerResult = testListCustomerResult.Generate(generatedQuantity);
        return listCustomerResult;
    }
}