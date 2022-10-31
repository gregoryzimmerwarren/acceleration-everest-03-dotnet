using Bogus;
using Bogus.Extensions.Brazil;
using DomainModels.Models;
using System.Collections.Generic;

namespace AppServices.Tests.Fixtures.Customers;

public class CustomerFixture
{
    public static Customer GenerateCustomerFixture()
    {
        var testCustomerDto = new Faker<Customer>("pt_BR")
            .CustomInstantiator(faker => new Customer(
                fullName: faker.Name.FirstName() + " " + faker.Name.LastName(),
                email: faker.Internet.Email(),
                cpf: faker.Person.Cpf(),
                cellphone: faker.Phone.PhoneNumberFormat(),
                country: faker.Address.Country(),
                city: faker.Address.City(),
                address: faker.Address.StreetAddress(),
                postalCode: faker.Address.ZipCode(),
                number: faker.Random.Number(min: 1),
                emailSms: faker.Random.Bool(),
                whatsapp: faker.Random.Bool(),
                dateOfBirth: faker.Date.Past(18)));

        var customerDto = testCustomerDto.Generate();
        return customerDto;
    }

    public static IEnumerable<Customer> GenerateListCustomerFixture(int generatedQuantity)
    {
        var testListCustomerDto = new Faker<Customer>("pt_BR")
            .CustomInstantiator(faker => new Customer(
                fullName: faker.Name.FirstName() + " " + faker.Name.LastName(),
                email: faker.Internet.Email(),
                cpf: faker.Person.Cpf(),
                cellphone: faker.Phone.PhoneNumberFormat(),
                country: faker.Address.Country(),
                city: faker.Address.City(),
                address: faker.Address.StreetAddress(),
                postalCode: faker.Address.ZipCode(),
                number: faker.Random.Number(min: 1),
                emailSms: faker.Random.Bool(),
                whatsapp: faker.Random.Bool(),
                dateOfBirth: faker.Date.Past(18)));

        var listCustomerDto = testListCustomerDto.Generate(generatedQuantity);
        return listCustomerDto;
    }
}