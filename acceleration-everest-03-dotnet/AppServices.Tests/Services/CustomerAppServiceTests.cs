using AppServices.Interfaces;
using AppServices.Services;
using AppServices.Tests.Fixtures.Customers;
using AutoMapper;
using Castle.Core.Resource;
using DomainModels.Models;
using DomainServices.Interfaces;
using DomainServices.Services;
using FluentAssertions;
using Moq;

namespace AppServicesTests.Services;

public class CustomerAppServiceTests
{
    private readonly Mock<ICustomerBankInfoAppService> _mockCustomerBankInfoAppService;
    private readonly Mock<ICustomerBankInfoService> _mockCustomerBankInfoService;
    private readonly CustomerBankInfoAppService _customerBankInfoAppService;
    private readonly Mock<ICustomerService> _mockCustomerService;
    private readonly CustomerAppService _customerAppService;
    private readonly Mock<IMapper> _mockMapper;

    public CustomerAppServiceTests()
    {
        _mockMapper = new Mock<IMapper>();
        _mockCustomerService = new Mock<ICustomerService>();
        _mockCustomerBankInfoService = new Mock<ICustomerBankInfoService>();
        _mockCustomerBankInfoAppService = new Mock<ICustomerBankInfoAppService>();
        _customerBankInfoAppService = new CustomerBankInfoAppService(_mockCustomerBankInfoService.Object, _mockMapper.Object);
        _customerAppService = new CustomerAppService(_mockCustomerBankInfoAppService.Object, _mockCustomerService.Object, _mockMapper.Object);
    }

    [Fact]
        public async void Should_Create_Successfully()
    {
        // Arrange  
        var createCustomerTest = CreateCustomerBogus.GenerateCreateCustomerBogus();
        Customer customer = CustomerBogus.GenerateCustomerBogus();
        long idTest = 1;
                     
        _mockCustomerService.Setup(customerService => customerService.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(idTest);
        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.Create(idTest)); 

        // Action
        var result = await _customerAppService.CreateAsync(createCustomerTest).ConfigureAwait(false);
        _customerBankInfoAppService.Create(idTest);

        // Assert
        result.Should().BeGreaterThan(0);

        _mockCustomerService.Verify(customerService => customerService.CreateAsync(It.IsAny<Customer>()), Times.Once);
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.Create(idTest), Times.Once);
    }

    [Fact]
    public async void Should_Delete_Successfully()
    {
        // Arrange  
        long idTest = 1;

        _mockCustomerService.Setup(customerService => customerService.DeleteAsync(It.IsAny<long>()));
        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.DeleteAsync(It.IsAny<long>()));

        // Action
        await _customerAppService.DeleteAsync(idTest).ConfigureAwait(false);
        await _customerBankInfoAppService.DeleteAsync(idTest).ConfigureAwait(false);

        // Assert
        _mockCustomerService.Verify(customerService => customerService.DeleteAsync(It.IsAny<long>()), Times.Once);
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.DeleteAsync(It.IsAny<long>()), Times.Once);
    }
}