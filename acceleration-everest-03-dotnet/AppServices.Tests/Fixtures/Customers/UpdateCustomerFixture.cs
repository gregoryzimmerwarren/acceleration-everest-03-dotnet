using AppModels.Customers;
using Bogus;
using Bogus.Extensions.Brazil;

namespace AppServices.Tests.Fixtures.Customers;

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
            number: faker.Random.Number(),
            emailSms: faker.Random.Bool(),
            whatsapp: faker.Random.Bool(),
            dateOfBirth: faker.Date.Past(18)));

        var UpdateCustomerDto = testUpdateCustomerDto.Generate();
        return UpdateCustomerDto;
    }
}