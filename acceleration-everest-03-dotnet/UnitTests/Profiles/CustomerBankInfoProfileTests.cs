using AppModels.CustomersBankInfo;
using AppServices.Profiles;
using AutoMapper;
using FluentAssertions;
using UnitTests.Fixtures.Customers;
using UnitTests.Fixtures.CustomersBankInfo;

namespace UnitTests.Profiles;

public class CustomerBankInfoProfileTests : CustomerBankInfoProfile
{
    private readonly IMapper _mapper;

    public CustomerBankInfoProfileTests()
    {
        _mapper = new MapperConfiguration(cfg => { cfg.AddProfile<CustomerBankInfoProfile>(); }).CreateMapper();
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