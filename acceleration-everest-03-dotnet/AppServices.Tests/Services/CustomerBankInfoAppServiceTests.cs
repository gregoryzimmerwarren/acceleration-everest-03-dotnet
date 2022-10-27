using AppModels.CustomersBankInfo;
using AppServices.Services;
using AppServices.Tests.Fixtures.Customers;
using AppServices.Tests.Fixtures.CustomersBankInfo;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using FluentAssertions;
using Moq;
using System.Collections.Generic;

namespace AppServices.Tests.Services;

public class CustomerBankInfoAppServiceTests
{
    private readonly Mock<ICustomerBankInfoService> _mockCustomerBankInfoService;
    private readonly CustomerBankInfoAppService _customerBankInfoAppService;
    private readonly IMapper _mapper;

    public CustomerBankInfoAppServiceTests()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.CreateMap<CustomerBankInfo, CustomerBankInfoResult>();
            cfg.CreateMap<CustomerBankInfo, CustomerBankInfoResultForCustomerDtos>();
        });
        _mapper = config.CreateMapper();
        _mockCustomerBankInfoService = new Mock<ICustomerBankInfoService>();
        _customerBankInfoAppService = new CustomerBankInfoAppService(_mockCustomerBankInfoService.Object, _mapper);
    }

    [Fact]
    public void Should_CreateCustomerBankInfo_Successfully()
    {
        // Arrange
        long idTest = 1;

        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.Create(It.IsAny<long>()));

        // Action
        _customerBankInfoAppService.Create(idTest);

        // Assert
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.Create(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_DeleteCustomerBankInfo_Successfully()
    {
        // Arrange
        long idTest = 1;

        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.DeleteAsync(It.IsAny<long>()));

        // Action
        await _customerBankInfoAppService.DeleteAsync(idTest).ConfigureAwait(false);

        // Assert
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.DeleteAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_DepositCustomerBankInfoAsync_Successfully()
    {
        // Arrage
        long idTest = 1;
        decimal amountTest = 17.05m;

        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()));
        
        // Action
        await _customerBankInfoAppService.DepositAsync(idTest, amountTest).ConfigureAwait(false);

        // Assert
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
    }

    [Fact]
    public async void Should_GetAllCustomersBankInfoAsync_Successfully()
    {
        // Arrange
        var listCustomerBankInfoResultTest = CustomerBankInfoResultFixture.GenerateListCustomerBankInfoResultFixture(3);
        var listCustomerBankInfoTest = CustomerBankInfoFixture.GenerateListCustomerBankInfoFixture(3);

        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.GetAllCustomersBankInfoAsync()).ReturnsAsync(listCustomerBankInfoTest);
        _mapper.Map<IEnumerable<CustomerBankInfoResult>>(listCustomerBankInfoTest);

        // Action
        await _customerBankInfoAppService.GetAllCustomersBankInfoAsync().ConfigureAwait(false);

        // Assert
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.GetAllCustomersBankInfoAsync(), Times.Once);
    }

    [Fact]
    public async void Should_GetCustomerBankInfoByCustomerIdAsync_Successfully()
    {
        // Arrange
        long idTest = 1;
        var customerBankInfoResultTest = CustomerBankInfoResultFixture.GenerateCustomerBankInfoResultFixture();
        var customerBankInfoTest = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();
        customerBankInfoTest.CustomerId = idTest;

        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.GetCustomerBankInfoByCustomerIdAsync(It.IsAny<long>())).ReturnsAsync(customerBankInfoTest);
        _mapper.Map<CustomerBankInfoResult>(customerBankInfoTest);

        // Action
        var result = await _customerBankInfoAppService.GetCustomerBankInfoByCustomerIdAsync(idTest).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.GetCustomerBankInfoByCustomerIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_GetTotalByCustomerIdAsync_Successfully()
    {
        // Arrange
        long idTest = 1;
        var customerTest = CustomerFixture.GenerateCustomerFixture();
        customerTest.Id = idTest;
        var customerBankInfoTest = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();

        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.GetTotalByCustomerIdAsync(It.IsAny<long>())).ReturnsAsync(It.IsAny<decimal>());

        // Action
        var result = await _customerBankInfoAppService.GetTotalByCustomerIdAsync(idTest).ConfigureAwait(false);

        // Assert
        result.Should().BeGreaterThanOrEqualTo(0);
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.GetTotalByCustomerIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_WithdrawCustomerBankInfoAsync_Successfully()
    {
        // Arrage
        long idTest = 1;
        decimal amountTest = 17.05m;

        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>())).ReturnsAsync(true);

        // Action
        var result = await _customerBankInfoAppService.WithdrawAsync(idTest, amountTest).ConfigureAwait(false);

        // Assert
        result.Should().BeTrue();
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
    }
}