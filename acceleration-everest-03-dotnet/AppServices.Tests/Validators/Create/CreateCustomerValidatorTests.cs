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
    public void Should_NotCreateCustomer_Invalid_FullName_Empty_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_FullName_LessThan5Characters_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_Email_Empty_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_Email_Format_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_EmailConfirmation_DifferentFromEmail_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_Cpf_Empty_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_Cpf_Size_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_Cpf_IdenticalDigits_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_Cellphone_Empty_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_Cellphone_MoreThan14Characters_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_Cellphone_Size_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_Cellphone_DigitDifferentThen9_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_Cellphone_DigitDifferentThenNumber_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_Country_Empty_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_Country_LessThan3Characters_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_City_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_Address_Empty_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_Address_LessThan4Characters_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_PostalCode_Empty_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_PostalCode_MoreThan9Characters_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_PostalCode_Size_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_PostalCode_DigitDifferentThenNumber_Successfully()
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
    public void Should_NotCreateCustomer_Invalid_DateOfBirth_LessThan18YearsOld_Successfully()
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
}