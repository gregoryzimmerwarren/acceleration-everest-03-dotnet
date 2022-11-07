using AppModels.CustomersBankInfo;
using AppServices.Services;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using FluentAssertions;
using Moq;
using UnitTests.Fixtures.Customers;
using UnitTests.Fixtures.CustomersBankInfo;

namespace UnitTests.AppServices;

public class CustomerBankInfoAppServiceTests
{
    private readonly Mock<ICustomerBankInfoService> _mockCustomerBankInfoService;
    private readonly CustomerBankInfoAppService _customerBankInfoAppService;
    private readonly IMapper _mapper;

    public CustomerBankInfoAppServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
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
        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.Create(It.IsAny<long>()));

        // Action
        _customerBankInfoAppService.Create(It.IsAny<long>());

        // Assert
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.Create(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_DeleteCustomerBankInfo_Successfully()
    {
        // Arrange
        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.DeleteAsync(It.IsAny<long>()));

        // Action
        await _customerBankInfoAppService.DeleteAsync(It.IsAny<long>()).ConfigureAwait(false);

        // Assert
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.DeleteAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_DepositCustomerBankInfoAsync_Successfully()
    {
        // Arrage
        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()));

        // Action
        await _customerBankInfoAppService.DepositAsync(It.IsAny<long>(), It.IsAny<decimal>()).ConfigureAwait(false);

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
    public async void Should_GetAccountBalanceByCustomerIdAsync_Successfully()
    {
        // Arrange
        var customerTest = CustomerFixture.GenerateCustomerFixture();
        var customerBankInfoTest = CustomerBankInfoFixture.GenerateCustomerBankInfoFixture();

        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.GetAccountBalanceByCustomerIdAsync(It.IsAny<long>())).ReturnsAsync(It.IsAny<decimal>());

        // Action
        var result = await _customerBankInfoAppService.GetAccountBalanceByCustomerIdAsync(It.IsAny<long>()).ConfigureAwait(false);

        // Assert
        result.Should().BeGreaterThanOrEqualTo(0);
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.GetAccountBalanceByCustomerIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_WithdrawCustomerBankInfoAsync_Successfully()
    {
        // Arrage
        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>())).ReturnsAsync(true);

        // Action
        var result = await _customerBankInfoAppService.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>()).ConfigureAwait(false);

        // Assert
        result.Should().BeTrue();
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.WithdrawAsync(It.IsAny<long>(), It.IsAny<decimal>()), Times.Once);
    }
}