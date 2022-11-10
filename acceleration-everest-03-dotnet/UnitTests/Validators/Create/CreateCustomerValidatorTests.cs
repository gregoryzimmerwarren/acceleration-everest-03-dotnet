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

    [Fact]
    public void Should_NotCreateCustomer_When_FullName_Empty()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.FullName = string.Empty;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.FullName);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_FullName_LessThan5Characters()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.FullName = "Ruth";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.FullName);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Email_Empty()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Email = string.Empty;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Email);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Email_Format()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Email = "wrongEmail.com";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Email);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_EmailConfirmation_DifferentFromEmail()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.EmailConfirmation = "other@email.com";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cpf_Empty()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cpf = string.Empty;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Cpf);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cpf_Size()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cpf = "085";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Cpf);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cpf_IdenticalDigits()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cpf = "11111111111";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Cpf);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cellphone_Empty()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cellphone = string.Empty;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Cellphone);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cellphone_MoreThan14Characters()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cellphone = "(47)99999-99999";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Cellphone);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cellphone_Size()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cellphone = "(47)999-9999";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Cellphone);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cellphone_DigitDifferentThen9()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cellphone = "(47)89999-9999";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Cellphone);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cellphone_DigitDifferentThenNumber()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cellphone = "(47)9a999-9999";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Cellphone);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Country_Empty()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Country = string.Empty;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Country);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Country_LessThan3Characters()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Country = "Ab";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Country);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_City()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.City = string.Empty;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.City);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Address_Empty()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Address = string.Empty;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Address);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Address_LessThan4Characters()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Address = "Rua";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.Address);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_PostalCode_Empty()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.PostalCode = string.Empty;

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.PostalCode);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_PostalCode_MoreThan9Characters()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.PostalCode = "11111-1111";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.PostalCode);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_PostalCode_Size()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.PostalCode = "111-11";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.PostalCode);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_PostalCode_DigitDifferentThenNumber()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.PostalCode = "1a111-111";

        // Action
        var result = _validCreateCustomer.TestValidate(createCustomerTest);

        // Assert
        result.ShouldHaveValidationErrorFor(createCustomer => createCustomer.PostalCode);
    }

    [Fact]
    public void Should_NotCreateCustomer_When_DateOfBirth_BornLessThan18YearsAgo()
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
    public void Should_NotCreateCustomer_When_DateOfBirth_Born18YearsAgo_ButNotTurned18Yet()
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