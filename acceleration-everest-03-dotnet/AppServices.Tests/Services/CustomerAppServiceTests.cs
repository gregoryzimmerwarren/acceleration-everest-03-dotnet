using AppServices.Services;
using AppServices.Tests.Fixtures;
using AutoMapper;
using DomainServices.Interfaces;
using FluentAssertions;
using Moq;

namespace AppServicesTests.Services;

public class CustomerAppServiceTests
{
    private readonly ICustomerBankInfoService _customerBankInfoService;
    private readonly CustomerAppService _customerAppService;
    private readonly IPortfolioService _portfolioService;
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;

    public CustomerAppServiceTests()
    {
        _customerBankInfoService = Mock.Of<ICustomerBankInfoService>(MockBehavior.Default);
        _portfolioService = Mock.Of<IPortfolioService>(MockBehavior.Default);
        _customerService = Mock.Of<ICustomerService>(MockBehavior.Default);
        _mapper = Mock.Of<IMapper>(MockBehavior.Default);

        _customerAppService = new CustomerAppService(_customerBankInfoService, _portfolioService, _customerService, _mapper);

    }
        
    [Fact]
    public void Should_Create_Successfully()
    {
        // Arrange            
        var customerDtoTest = CustomerBogus.GenerateCustomerDto();

        // Action
        var result = _customerAppService.Create(customerDtoTest);

        // Assert
        result.Should().BeGreaterThan(0);  
    }
}