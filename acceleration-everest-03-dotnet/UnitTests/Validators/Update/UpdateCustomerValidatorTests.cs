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

    [Fact]
    public void ShouldNot_UpdateCustomer_When_FullName_Empty()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.FullName = string.Empty;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.FullName);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_FullName_LessThan5Characters()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.FullName = "Ruth";

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.FullName);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_Email_Empty()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Email = string.Empty;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Email);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_Email_Format()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Email = "wrongemail.com";

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

    [Fact]
    public void ShouldNot_UpdateCustomer_When_Cpf_Empty()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cpf = string.Empty;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Cpf);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_Cpf_Size()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cpf = "085";

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Cpf);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_Cpf_IdenticalDigits()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cpf = "11111111111";

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Cpf);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_Cellphone_Empty()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cellphone = string.Empty;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Cellphone);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_Cellphone_MoreThan14Characters()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cellphone = "(47)99999-99999";

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Cellphone);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_Cellphone_Size()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cellphone = "(47)999-9999";

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Cellphone);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_Cellphone_DigitDifferentThen9()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cellphone = "(47)89999-9999";

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Cellphone);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_Cellphone_DigitDifferentThenNumber()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cellphone = "(47)9a999-9999";

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Cellphone);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_Country_Empty()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Country = string.Empty;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Country);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_Country_LessThan3Characters()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Country = "Ab";

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

    [Fact]
    public void ShouldNot_UpdateCustomer_When_Address_Empty()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Address = string.Empty;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Address);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_Address_LessThan4Characters()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Address = "Rua";

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.Address);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_PostalCode_Empty()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.PostalCode = string.Empty;

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.PostalCode);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_PostalCode_MoreThan9Characters()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.PostalCode = "11111-1111";

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.PostalCode);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_PostalCode_Size()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.PostalCode = "111-11";

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.PostalCode);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_PostalCode_DigitDifferentThenNumber()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.PostalCode = "1a111-111";

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.PostalCode);
    }

    [Fact]
    public void ShouldNot_UpdateCustomer_When_DateOfBirth_LessThan18YearsOld()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.DateOfBirth = DateTime.Now.AddYears(-10);

        // Action
        var result = _validUpdateCustomer.TestValidate(updateCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(updateCustomer => updateCustomer.DateOfBirth);
    }
}