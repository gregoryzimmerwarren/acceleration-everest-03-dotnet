using AppModels.Customers;
using AppServices.Interfaces;
using AppServices.Services;
using AppServices.Tests.Fixtures.Customers;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using FluentAssertions;
using Moq;

namespace AppServicesTests.Services;

public class CustomerAppServiceTests
{
    private readonly Mock<ICustomerBankInfoAppService> _mockCustomerBankInfoAppService;
    private readonly Mock<ICustomerService> _mockCustomerService;
    private readonly CustomerAppService _customerAppService;
    private readonly Mock<IMapper> _mockMapper;

    public CustomerAppServiceTests()
    {
        _mockCustomerBankInfoAppService = new Mock<ICustomerBankInfoAppService>();
        _mockMapper = new Mock<IMapper>();
        _mockCustomerService = new Mock<ICustomerService>();
        _customerAppService = new CustomerAppService(_mockCustomerBankInfoAppService.Object, _mockCustomerService.Object, _mockMapper.Object);
    }

    [Fact]
        public async void Should_Create_Successfully()
    {
        // Arrange  
        var createCustomerTest = CreateCustomerBogus.GenerateCreateCustomerBogus();
        Customer customer = CustomerBogus.GenerateCustomerBogus();
        long idTest = 1;
                     
        _mockCustomerService.Setup(service => service.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(idTest);
        _mockCustomerBankInfoAppService.Setup(appService => appService.Create(idTest));

        // Action
        var result = await _customerAppService.CreateAsync(createCustomerTest);

        // Assert
        result.Should().BeGreaterThan(0);

        _mockCustomerService.Verify(service => service.CreateAsync(It.IsAny<Customer>()), Times.Once);
        _mockCustomerBankInfoAppService.Verify(appService => appService.Create(idTest), Times.Once);
    }
}