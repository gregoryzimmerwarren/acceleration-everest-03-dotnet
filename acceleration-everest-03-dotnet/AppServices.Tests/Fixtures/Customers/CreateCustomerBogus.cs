using AppModels.Customers;
using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using System;

namespace AppServices.Tests.Fixtures.Customers;

public class CreateCustomerBogus
{
    public static CreateCustomer GenerateCreateCustomerBogus()
    {
        var testCustomerDto = new Faker<CreateCustomer>("pt_BR")
            .CustomInstantiator(faker => new CreateCustomer(
            fullName: faker.Name.FirstName() + " " + faker.Name.LastName(),
            email: faker.Internet.Email(),
            emailConfirmation: faker.Internet.Email(),
            cpf: faker.Person.Cpf(),
            cellphone: faker.Phone.PhoneNumberFormat(),
            country: faker.Address.Country(),
            city: faker.Address.City(),
            address: faker.Address.StreetAddress(),
            postalCode: faker.Address.ZipCode(),
            number: faker.Random.Number(),
            emailSms: faker.Random.Bool(),
            whatsapp: faker.Random.Bool(),
            dateOfBirth: faker.Date.Past(18)));

        var customerDto = testCustomerDto.Generate();
        return customerDto;
    }
}