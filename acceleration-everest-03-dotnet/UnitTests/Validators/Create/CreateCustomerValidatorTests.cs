using AppServices.Validators.Create;
using FluentAssertions;
using FluentValidation.TestHelper;
using UnitTests.Fixtures.Customers;

namespace UnitTests.Validators.Create;

public class CreateCustomerValidatorTests
{
    private readonly CreateCustomerValidator _validCreateCustomer;

    public CreateCustomerValidatorTests()
    {
        _validCreateCustomer = new CreateCustomerValidator();
    }

    [Fact]
    public void Should_CreateCustomer_Valid_Successfully()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();

        // Action
        var result = _validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("Ruth")]
    public void ShouldNot_CreateCustomer_When_FullName_IsEmpty_Or_LessThan5Characters(string name)
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.FullName = name;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.FullName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("wrongEmail.com")]
    public void ShouldNot_CreateCustomer_When_Email_Empty_Or_WithWrongFormat(string email)
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Email = email;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Email);
    }

    [Fact]
    public void ShouldNot_CreateCustomer_When_EmailConfirmation_DifferentFromEmail()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.EmailConfirmation = "other@email.com";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer);
    }

    [Theory]
    [InlineData("")]
    [InlineData("085")]
    [InlineData("11111111111")]
    public void ShouldNot_CreateCustomer_When_Cpf_Empty_WithWrongSize_Or_WithIdenticalDigits(string cpf)
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cpf = cpf;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Cpf);
    }

    [Theory]
    [InlineData("")]
    [InlineData("(47)99999-99999")]
    [InlineData("(47)999-9999")]
    [InlineData("(47)89999-9999")]
    [InlineData("(47)9a999-9999")]
    public void ShouldNot_CreateCustomer_When_Cellphone_Empty_Or_MoreThan14Characters_Or_WrongSize_Or_FirstDigitDifferentThen9_Or_FirstDigitDifferentThenNumber(string cellphone)
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cellphone = cellphone;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Cellphone);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Ab")]
    public void ShouldNot_CreateCustomer_When_Country_Empty_Or_LessThan3Characters(string country)
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Country = country;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Country);
    }

    [Fact]
    public void ShouldNot_CreateCustomer_When_City()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.City = string.Empty;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.City);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Rua")]
    public void ShouldNot_CreateCustomer_When_Address_Empty_Or_LessThan4Characters(string address)
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Address = address;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Address);
    }

    [Theory]
    [InlineData("")]
    [InlineData("11111-1111")]
    [InlineData("111-11")]
    [InlineData("1a111-111")]
    public void ShouldNot_CreateCustomer_When_PostalCode_Empty_Or_MoreThan9Characters_Or_WrongSize_Or_DigitDifferentThenNumber(string postalcode)
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.PostalCode = postalcode;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.PostalCode);
    }

    [Fact]
    public void ShouldNot_CreateCustomer_When_DateOfBirth_BornLessThan18YearsAgo()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.DateOfBirth = DateTime.Now.AddYears(-10);

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.DateOfBirth);
    }

    [Fact]
    public void ShouldNot_CreateCustomer_When_DateOfBirth_Born18YearsAgo_ButNotTurned18Yet()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.DateOfBirth = DateTime.Now.AddDays(1).AddYears(-18);

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.DateOfBirth);
    }
}