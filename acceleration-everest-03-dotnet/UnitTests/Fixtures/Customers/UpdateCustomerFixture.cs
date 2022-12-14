using AppModels.Customers;
using Bogus;
using Bogus.Extensions.Brazil;
using System;

namespace UnitTests.Fixtures.Customers;

public class UpdateCustomerFixture
{
    public static UpdateCustomer GenerateUpdateCustomerFixture()
    {
        var testUpdateCustomerDto = new Faker<UpdateCustomer>("pt_BR")
            .CustomInstantiator(faker => new UpdateCustomer(
            fullName: faker.Name.FirstName() + " " + faker.Name.LastName(),
            email: faker.Internet.Email(),
            emailConfirmation: faker.Internet.Email(),
            cpf: faker.Person.Cpf(),
            cellphone: faker.Phone.PhoneNumberFormat(),
            country: faker.Address.Country(),
            city: faker.Address.City(),
            address: faker.Address.StreetAddress(),
            postalCode: faker.Address.ZipCode(),
            number: faker.Random.Number(min: 1),
            emailSms: faker.Random.Bool(),
            whatsapp: faker.Random.Bool(),
            dateOfBirth: faker.Date.Between(start: DateTime.Today.AddYears(-18),
            end: DateTime.Today.AddYears(-80))))
            .RuleFor(customerCreate => customerCreate.EmailConfirmation,
            (customerCreate, faker) => faker.Email);

        var UpdateCustomerDto = testUpdateCustomerDto.Generate();
        return UpdateCustomerDto;
    }
}