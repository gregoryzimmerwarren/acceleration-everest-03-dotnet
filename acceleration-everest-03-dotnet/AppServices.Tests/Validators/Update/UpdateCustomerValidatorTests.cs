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
    public void Should_NotUpdateCustomer_When_FullName_Empty()
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
    public void Should_NotUpdateCustomer_When_FullName_LessThan5Characters()
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
    public void Should_NotUpdateCustomer_When_Email_Empty()
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
    public void Should_NotUpdateCustomer_When_Email_Format()
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
    public void Should_NotUpdateCustomer_When_EmailConfirmation_DifferentFromEmail()
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
    public void Should_NotUpdateCustomer_When_Cpf_Empty()
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
    public void Should_NotUpdateCustomer_When_Cpf_Size()
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
    public void Should_NotUpdateCustomer_When_Cpf_IdenticalDigits()
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
    public void Should_NotUpdateCustomer_When_Cellphone_Empty()
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
    public void Should_NotUpdateCustomer_When_Cellphone_MoreThan14Characters()
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
    public void Should_NotUpdateCustomer_When_Cellphone_Size()
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
    public void Should_NotUpdateCustomer_When_Cellphone_DigitDifferentThen9()
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
    public void Should_NotUpdateCustomer_When_Cellphone_DigitDifferentThenNumber()
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
    public void Should_NotUpdateCustomer_When_Country_Empty()
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
    public void Should_NotUpdateCustomer_When_Country_LessThan3Characters()
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
    public void Should_NotUpdateCustomer_When_City()
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
    public void Should_NotUpdateCustomer_When_Address_Empty()
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
    public void Should_NotUpdateCustomer_When_Address_LessThan4Characters()
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
    public void Should_NotUpdateCustomer_When_PostalCode_Empty()
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
    public void Should_NotUpdateCustomer_When_PostalCode_MoreThan9Characters()
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
    public void Should_NotUpdateCustomer_When_PostalCode_Size()
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
    public void Should_NotUpdateCustomer_When_PostalCode_DigitDifferentThenNumber()
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
    public void Should_NotUpdateCustomer_When_DateOfBirth_LessThan18YearsOld()
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