using AppServices.Tests.Fixtures.Customers;
using AppServices.Validators.Update;
using FluentAssertions;
using System;

namespace AppServices.Tests.Validators.Update;

public class UpdateCustomerValidatorTests
{
    [Fact]
    public void Should_UpdateCustomer_Valid_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_FullName_Empty_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.FullName = "";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_FullName_LessThan5Characters_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.FullName = "Ruth";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_Email_Empty_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Email = "";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_Email_Format_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Email = "wrongemail.com";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_EmailConfirmation_DifferentFromEmail_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.EmailConfirmation = "other@email.com";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_Cpf_Empty_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cpf = "";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_Cpf_Size_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cpf = "085";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_Cpf_IdenticalDigits_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cpf = "11111111111";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_Cellphone_Empty_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cellphone = "";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_Cellphone_MoreThan14Characters_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cellphone = "(47)99999-99999";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_Cellphone_Size_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cellphone = "(47)999-9999";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_Cellphone_DigitDifferentThen9_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cellphone = "(47)89999-9999";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_Cellphone_DigitDifferentThenNumber_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Cellphone = "(47)9a999-9999";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_Country_Empty_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Country = "";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_Country_LessThan3Characters_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Country = "Ab";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_City_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.City = "";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_Address_Empty_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Address = "";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_Address_LessThan4Characters_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.Address = "Rua";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_PostalCode_Empty_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.PostalCode = "";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_PostalCode_MoreThan9Characters_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.PostalCode = "11111-1111";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_PostalCode_Size_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.PostalCode = "111-11";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_PostalCode_DigitDifferentThenNumber_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.PostalCode = "1a111-111";
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_NotUpdateCustomer_Invalid_DateOfBirth_LessThan18YearsOld_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        updateCustomerTest.DateOfBirth = DateTime.Now.AddYears(-10);
        var validUpdateCustomer = new UpdateCustomerValidator();

        // Action
        var result = validUpdateCustomer.Validate(updateCustomerTest);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}