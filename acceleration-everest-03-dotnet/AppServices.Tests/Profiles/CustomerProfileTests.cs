using AppModels.Customers;
using AppServices.Profiles;
using AppServices.Tests.Fixtures.Customers;
using AppServices.Tests.Fixtures.CustomersBankInfo;
using AppServices.Tests.Fixtures.Portfolios;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;

namespace AppServices.Tests.Profiles;

public class CustomerProfileTests : CustomerProfile
{
    private readonly IMapper _mapper;
    
    public CustomerProfileTests()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.CreateMap<Customer, CustomerResult>();
            cfg.CreateMap<Customer, CustomerResultForOtherDtos>();
            cfg.CreateMap<CreateCustomer, Customer>();
            cfg.CreateMap<UpdateCustomer, Customer>();
        });
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Should_MapTo_CustomerResult_FromCustomer_Successfully()
    {
        // Arrange
        var customerBankInfoResultForCustomerDtosTest = CustomerBankInfoResultForCustomerDtosFixture.GenerateCustomerBankInfoResultForCustomerDtosFixture();
        var portfolioResultForOthersDtosTest = PortfolioResultForOthersDtosFixture.GenerateListPortfolioResultForOthersDtosFixture(2);
        var customerTest = CustomerFixture.GenerateCustomerFixture();
        var customerResultTest = new CustomerResult(
            id: customerTest.Id,
            fullName: customerTest.FullName,
            email: customerTest.Email,
            cpf: customerTest.Cpf,
            cellphone: customerTest.Cellphone,
            city: customerTest.City,
            postalCode: customerTest.PostalCode,
            customerBankInfo: customerBankInfoResultForCustomerDtosTest,
            portfolios: portfolioResultForOthersDtosTest);

        // Action
        var result = _mapper.Map<CustomerResult>(customerTest);
        result.CustomerBankInfo = customerBankInfoResultForCustomerDtosTest;
        result.Portfolios = portfolioResultForOthersDtosTest;

        // Assert
        result.Should().BeEquivalentTo(customerResultTest);
    }

    [Fact]
    public void Should_MapTo_CustomerResultForOtherDtos_FromCustomer_Successfully()
    {
        // Arrange
        var customerTest = CustomerFixture.GenerateCustomerFixture();
        var customerResultTest = new CustomerResultForOtherDtos(
            id: customerTest.Id,
            fullName: customerTest.FullName,
            email: customerTest.Email,
            cpf: customerTest.Cpf,
            cellphone: customerTest.Cellphone,
            city: customerTest.City,
            postalCode: customerTest.PostalCode);

        // Action
        var result = _mapper.Map<CustomerResultForOtherDtos>(customerTest);

        // Assert
        result.Should().BeEquivalentTo(customerResultTest);
    }

    [Fact]
    public void Should_MapTo_Customers_FromCreateCustomer_Successfully()
    {
        // Arrange
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        var customerTest = new Customer(
            fullName: createCustomerTest.FullName,
            email: createCustomerTest.Email,
            cpf: createCustomerTest.Cpf,
            cellphone: createCustomerTest.Cellphone,
            country: createCustomerTest.Country,
            city: createCustomerTest.City,
            address: createCustomerTest.Address,
            postalCode: createCustomerTest.PostalCode,
            number: createCustomerTest.Number,
            emailSms: createCustomerTest.EmailSms,
            whatsapp: createCustomerTest.Whatsapp,
            dateOfBirth: createCustomerTest.DateOfBirth);

        // Action
        var result = _mapper.Map<Customer>(createCustomerTest);

        // Assert
        result.Should().BeEquivalentTo(customerTest);
    }
    
    [Fact]
    public void Should_MapTo_Customers_FromUpdateCustomer_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        var customerTest = new Customer(
            fullName: updateCustomerTest.FullName,
            email: updateCustomerTest.Email,
            cpf: updateCustomerTest.Cpf,
            cellphone: updateCustomerTest.Cellphone,
            country: updateCustomerTest.Country,
            city: updateCustomerTest.City,
            address: updateCustomerTest.Address,
            postalCode: updateCustomerTest.PostalCode,
            number: updateCustomerTest.Number,
            emailSms: updateCustomerTest.EmailSms,
            whatsapp: updateCustomerTest.Whatsapp,
            dateOfBirth: updateCustomerTest.DateOfBirth);

        // Action
        var result = _mapper.Map<Customer>(updateCustomerTest);

        // Assert
        result.Should().BeEquivalentTo(customerTest);
    }
}