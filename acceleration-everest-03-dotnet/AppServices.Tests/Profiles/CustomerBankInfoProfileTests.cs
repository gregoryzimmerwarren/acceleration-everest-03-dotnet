using AppModels.CustomersBankInfo;
using AppServices.Tests.Fixtures.Customers;
using AppServices.Tests.Fixtures.CustomersBankInfo;
using AutoMapper;
using DomainModels.Models;
using FluentAssertions;

namespace AppServices.Tests.Profiles;

public class CustomerBankInfoProfileTests
{    
    private readonly IMapper _mapper;

    public CustomerBankInfoProfileTests()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.CreateMap<CustomerBankInfo, CustomerBankInfoResult>();
            cfg.CreateMap<CustomerBankInfo, CustomerBankInfoResultForCustomerDtos>();
        });
        _mapper = config.CreateMapper();       
    }

    [Fact]
    public void Should_MapTo_CustomerBankInfoResult_FromCustomerBankInfo_Successfully()
    {
        // Arrange
        var customerResultForOtherDtosTest = CustomerResultForOtherDtosFixture.GenerateCustomerResultForOtherDtosFixture();
        var customerBankInfoTest = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();
        var customerBankInfoResultTest = new CustomerBankInfoResult(
            id: customerBankInfoTest.Id,
            accountBalance: customerBankInfoTest.AccountBalance,
            customer: customerResultForOtherDtosTest);

        // Action
        var result = _mapper.Map<CustomerBankInfoResult>(customerBankInfoTest);
        result.Customer = customerResultForOtherDtosTest;

        // Assert
        result.Should().BeEquivalentTo(customerBankInfoResultTest);
    }

    [Fact]
    public void Should_MapTo_CustomerBankInfoResultForCustomerDtos_FromCustomerBankInfo_Successfully()
    {
        // Arrange
        var customerBankInfoTest = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();
        var customerBankInfoResultForCustomerDtosTest = new CustomerBankInfoResultForCustomerDtos(
            id: customerBankInfoTest.Id,
            accountBalance: customerBankInfoTest.AccountBalance);

        // Action
        var result = _mapper.Map<CustomerBankInfoResultForCustomerDtos>(customerBankInfoTest);

        // Assert
        result.Should().BeEquivalentTo(customerBankInfoResultForCustomerDtosTest);
    }
}
