using AppModels.Customers;
using Bogus;
using Newtonsoft.Json;

namespace AppServices.Tests.Fixtures
{
    public static class CustomerBogus
    {
        public static CreateCustomerDto GenerateCustomerDto()
        {
            var testCustomerDto = new Faker<CreateCustomerDto>("pt_BR")
                .RuleFor(customer => customer.FullName, faker => faker.Name.FirstName() + " " + faker.Name.LastName())
                .RuleFor(customer => customer.Email, (faker, customer) => faker.Internet.Email(customer.FullName))
                .RuleFor(customer => customer.EmailConfirmation, (faker, customer) => (customer.Email))
                .RuleFor(customer => customer.Cellphone, faker => faker.Phone.PhoneNumberFormat())
                .RuleFor(customer => customer.Country, faker => faker.Address.Country())
                .RuleFor(customer => customer.City, faker => faker.Address.City())
                .RuleFor(customer => customer.Address, faker => faker.Address.StreetAddress())
                .RuleFor(customer => customer.PostalCode, faker => faker.Address.ZipCode())
                .RuleFor(customer => customer.Number, faker => faker.Random.Number())
                .RuleFor(customer => customer.EmailSms, faker => faker.Random.Bool())
                .RuleFor(customer => customer.Whatsapp, faker => faker.Random.Bool())
                .RuleFor(customer => customer.DateOfBirth, faker => faker.Date.Past(18));

            var customerDto = testCustomerDto.Generate();
            return customerDto;
        }

    }

    public static class ExtensionsForTesting
    {
        public static string DumpString(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
