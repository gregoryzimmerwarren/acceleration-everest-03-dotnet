using AppModels.Customers;
using AppServices.Interfaces;
using AppServices.Profiles;
using AppServices.Services;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using FluentAssertions;
using Moq;
using UnitTests.Fixtures.Customers;

namespace UnitTests.AppServices;

public class CustomerAppServiceTests
{
    private readonly Mock<ICustomerBankInfoAppService> _mockCustomerBankInfoAppService;
    private readonly Mock<ICustomerBankInfoService> _mockCustomerBankInfoService;
    private readonly CustomerBankInfoAppService _customerBankInfoAppService;
    private readonly Mock<ICustomerService> _mockCustomerService;
    private readonly CustomerAppService _customerAppService;
    private readonly IMapper _mapper;

    public CustomerAppServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Customer, CustomerResult>();
            cfg.CreateMap<CreateCustomer, Customer>();
            cfg.CreateMap<UpdateCustomer, Customer>();
        });
        _mapper = config.CreateMapper();
        _mockCustomerService = new Mock<ICustomerService>();
        _mockCustomerBankInfoService = new Mock<ICustomerBankInfoService>();
        _mockCustomerBankInfoAppService = new Mock<ICustomerBankInfoAppService>();
        _customerBankInfoAppService = new CustomerBankInfoAppService(_mockCustomerBankInfoService.Object, _mapper);
        _customerAppService = new CustomerAppService(_mockCustomerBankInfoAppService.Object, _mockCustomerService.Object, _mapper);
    }

    [Fact]
    public async void Should_CreateCustomer_Successfully()
    {
        // Arrange  
        var createCustomerTest = CreateCustomerFixture.GenerateCreateCustomerFixture();
        var customerTest = CustomerFixture.GenerateCustomerFixture();

        _mockCustomerService.Setup(customerService => customerService.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(customerTest.Id);
        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.Create(It.IsAny<long>()));

        // Action
        var result = await _customerAppService.CreateAsync(createCustomerTest).ConfigureAwait(false);
        _customerBankInfoAppService.Create(result);

        // Assert
        result.Should().NotBe(0);

        _mockCustomerService.Verify(customerService => customerService.CreateAsync(It.IsAny<Customer>()), Times.Once);
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.Create(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_DeleteCustomer_Successfully()
    {
        // Arrange
        _mockCustomerService.Setup(customerService => customerService.DeleteAsync(It.IsAny<long>()));
        _mockCustomerBankInfoService.Setup(customerBankInfoService => customerBankInfoService.DeleteAsync(It.IsAny<long>()));

        // Action
        await _customerAppService.DeleteAsync(It.IsAny<long>()).ConfigureAwait(false);
        await _customerBankInfoAppService.DeleteAsync(It.IsAny<long>()).ConfigureAwait(false);

        // Assert
        _mockCustomerService.Verify(customerService => customerService.DeleteAsync(It.IsAny<long>()), Times.Once);
        _mockCustomerBankInfoService.Verify(customerBankInfoService => customerBankInfoService.DeleteAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_GetAllCustomersAsync_Successfully()
    {
        // Arrange        
        var listCustomersResultTest = CustomerResultFixture.GenerateListCustomerResultFixture(3);
        var listCustomerTest = CustomerFixture.GenerateListCustomerFixture(3);

        _mockCustomerService.Setup(customerService => customerService.GetAllCustomersAsync()).ReturnsAsync(listCustomerTest);
        _mapper.Map<IEnumerable<CustomerResult>>(listCustomerTest);

        // Action
        var result = await _customerAppService.GetAllCustomersAsync().ConfigureAwait(false);

        // Assert
        result.Should().HaveCountGreaterThanOrEqualTo(3);
        _mockCustomerService.Verify(customerService => customerService.GetAllCustomersAsync(), Times.Once);
    }

    [Fact]
    public async void Should_GetCustomerByIdAsync_Successfully()
    {
        // Arrange
        var customerTest = CustomerFixture.GenerateCustomerFixture();

        _mockCustomerService.Setup(customerService => customerService.GetCustomerByIdAsync(It.IsAny<long>())).ReturnsAsync(customerTest);
        _mapper.Map<CustomerResult>(customerTest);

        // Action
        var result = await _customerAppService.GetCustomerByIdAsync(It.IsAny<long>()).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        _mockCustomerService.Verify(customerService => customerService.GetCustomerByIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public async void Should_UpdateCustomerAsync_Successfully()
    {
        // Arrange
        var updateCustomerTest = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
        var customerTest = CustomerFixture.GenerateCustomerFixture();

        _mockCustomerService.Setup(customerService => customerService.UpdateAsync(It.IsAny<Customer>()));

        // Action
        await _customerAppService.UpdateAsync(It.IsAny<long>(), updateCustomerTest).ConfigureAwait(false);

        // Assert
        _mockCustomerService.Verify(customerService => customerService.UpdateAsync(It.IsAny<Customer>()), Times.Once);
    }
}