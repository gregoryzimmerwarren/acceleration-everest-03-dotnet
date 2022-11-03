using AppModels.Customers;
using Bogus;
using Bogus.Extensions.Brazil;
using System.Collections.Generic;

namespace UnitTests.Fixtures.Customers;

public class CustomerResultForOtherDtosFixture
{
    public static CustomerResultForOtherDtos GenerateCustomerResultForOtherDtosFixture()
    {
        var testCustomerResultForOtherDtos = new Faker<CustomerResultForOtherDtos>("pt_BR")
            .CustomInstantiator(faker => new CustomerResultForOtherDtos(
                id: faker.Random.Long(0, 10),
                fullName: faker.Name.FirstName() + " " + faker.Name.LastName(),
                email: faker.Internet.Email(),
                cpf: faker.Person.Cpf(),
                cellphone: faker.Phone.PhoneNumberFormat(),
                city: faker.Address.City(),
                postalCode: faker.Address.ZipCode()));

        var customerResult = testCustomerResultForOtherDtos.Generate();
        return customerResult;
    }

    public static IEnumerable<CustomerResultForOtherDtos> GenerateListCustomerResultForOtherDtosFixture(int generatedQuantity)
    {
        var testListCustomerResultForOtherDtos = new Faker<CustomerResultForOtherDtos>("pt_BR")
            .CustomInstantiator(faker => new CustomerResultForOtherDtos(
                id: faker.Random.Long(0, 10),
                fullName: faker.Name.FirstName() + " " + faker.Name.LastName(),
                email: faker.Internet.Email(),
                cpf: faker.Person.Cpf(),
                cellphone: faker.Phone.PhoneNumberFormat(),
                city: faker.Address.City(),
                postalCode: faker.Address.ZipCode()));

        var listCustomerResultForOtherDtos = testListCustomerResultForOtherDtos.Generate(generatedQuantity);
        return listCustomerResultForOtherDtos;
    }
}