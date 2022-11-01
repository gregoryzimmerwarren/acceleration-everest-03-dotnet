using AppServices.Tests.Fixtures.Customers;
using AppServices.Validators.Create;
using FluentAssertions;
using System;

namespace AppServices.Tests.Validators.Create;

public class CreateCustomerValidatorTests
{
    [Fact]
    public void Should_CreateCustomer_Valid_Successfully()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_FullName_Empty()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.FullName = "";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void Should_NotCreateCustomer_When_FullName_LessThan5Characters()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.FullName = "Ruth";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Email_Empty()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Email = "";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Email_Format()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Email = "wrongemail.com";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_EmailConfirmation_DifferentFromEmail()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.EmailConfirmation = "other@email.com";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cpf_Empty()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cpf = "";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cpf_Size()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cpf = "085";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cpf_IdenticalDigits()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cpf = "11111111111";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cellphone_Empty()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cellphone = "";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cellphone_MoreThan14Characters()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cellphone = "(47)99999-99999";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cellphone_Size()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cellphone = "(47)999-9999";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cellphone_DigitDifferentThen9()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cellphone = "(47)89999-9999";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Cellphone_DigitDifferentThenNumber()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Cellphone = "(47)9a999-9999";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Country_Empty()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Country = "";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Country_LessThan3Characters()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Country = "Ab";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_City()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.City = "";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Address_Empty()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Address = "";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_Address_LessThan4Characters()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.Address = "Rua";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_PostalCode_Empty()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.PostalCode = "";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_PostalCode_MoreThan9Characters()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.PostalCode = "11111-1111";        
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_PostalCode_Size()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.PostalCode = "111-11";
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_PostalCode_DigitDifferentThenNumber()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.PostalCode = "1a111-111";
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotCreateCustomer_When_DateOfBirth_BornLessThan18YearsAgo()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.DateOfBirth = DateTime.Now.AddYears(-10);
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void Should_NotCreateCustomer_When_DateOfBirth_Born18YearsAgo_ButNotTurned18Yet()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        createCustomerTest.DateOfBirth = DateTime.Now.AddDays(1).AddYears(-18);
        var validCreateCustomer = new CreateCustomerValidator();

        // Action
        var result = validCreateCustomer.Validate(createCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}