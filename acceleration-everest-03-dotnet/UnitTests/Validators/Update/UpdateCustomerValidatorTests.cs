using AppServices.Validators.Update;
using FluentAssertions;
using FluentValidation.TestHelper;
using UnitTests.Fixtures.Customers;

namespace UnitTests.Validators.Update;

public class UpdateCustomerValidatorTests
{
    private readonly UpdateCustomerValidator _validUpdateCustomer;

    public UpdateCustomerValidatorTests()
    {
        _validUpdateCustomer = new UpdateCustomerValidator();
    }

    [Fact]
    public void Should_UpdateCustomer_Valid_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();

        // Action
        var result = _validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("Ruth")]
    public void ShouldNot_UpdateCustomer_When_FullName_IsEmpty_Or_LessThan5Characters(string name)
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.FullName = name;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.FullName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("wrongEmail.com")]
    public void ShouldNot_UpdateCustomer_When_Email_Empty_Or_WithWrongFormat(string email)
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Email = email;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Email);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_EmailConfirmation_DifferentFromEmail()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.EmailConfirmation = "other@email.com";

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer);
    }

    [Theory]
    [InlineData("")]
    [InlineData("085")]
    [InlineData("11111111111")]
    public void ShouldNot_UpdateCustomer_When_Cpf_Empty_WithWrongSize_Or_WithIdenticalDigits(string cpf)
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cpf = cpf;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Cpf);
    }

    [Theory]
    [InlineData("")]
    [InlineData("(47)99999-99999")]
    [InlineData("(47)999-9999")]
    [InlineData("(47)89999-9999")]
    [InlineData("(47)9a999-9999")]
    public void ShouldNot_UpdateCustomer_When_Cellphone_Empty_Or_MoreThan14Characters_Or_WrongSize_Or_FirstDigitDifferentThen9_Or_FirstDigitDifferentThenNumber(string cellphone)
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cellphone = cellphone;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Cellphone);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Ab")]
    public void ShouldNot_UpdateCustomer_When_Country_Empty_Or_LessThan3Characters(string country)
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Country = country;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Country);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_City()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.City = string.Empty;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.City);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Rua")]
    public void ShouldNot_UpdateCustomer_When_Address_Empty_Or_LessThan4Characters(string address)
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Address = address;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Address);
    }

    [Theory]
    [InlineData("")]
    [InlineData("11111-1111")]
    [InlineData("111-11")]
    [InlineData("1a111-111")]
    public void ShouldNot_UpdateCustomer_When_PostalCode_Empty_Or_MoreThan9Characters_Or_WrongSize_Or_DigitDifferentThenNumber(string postalcode)
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.PostalCode = postalcode;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.PostalCode);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_DateOfBirth_BornLessThan18YearsAgo()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.DateOfBirth = DateTime.Now.AddYears(-10);

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.DateOfBirth);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_DateOfBirth_Born18YearsAgo_ButNotTurned18Yet()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.DateOfBirth = DateTime.Now.AddDays(1).AddYears(-18);

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.DateOfBirth);
    }
}