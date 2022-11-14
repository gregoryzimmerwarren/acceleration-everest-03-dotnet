using AppServices.DependencyInjections;
using AppServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.DependencyInjections
{
    public class AddAppServicesConfigurationTests
    {
        [Fact]
        public void ICustomerBankInfoAppService_Registered_AsTransient_Successfully()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddAppServicesConfiguration();

            // Assert
            var result = Assert.Single(services, customerBankInfoAppService =>
                customerBankInfoAppService.ServiceType == typeof(ICustomerBankInfoAppService) &&
                customerBankInfoAppService.Lifetime == ServiceLifetime.Transient);
        }
        
        [Fact]
        public void ICustomerAppService_Registered_AsTransient_Successfully()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddAppServicesConfiguration();

            // Assert
            var result = Assert.Single(services, customerAppService =>
                customerAppService.ServiceType == typeof(ICustomerAppService) &&
                customerAppService.Lifetime == ServiceLifetime.Transient);
        }
        
        [Fact]
        public void IOrderAppService_Registered_AsTransient_Successfully()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddAppServicesConfiguration();

            // Assert
            var result = Assert.Single(services, orderAppService =>
                orderAppService.ServiceType == typeof(IOrderAppService) &&
                orderAppService.Lifetime == ServiceLifetime.Transient);
        }
        
        [Fact]
        public void IPortfolioAppService_Registered_AsTransient_Successfully()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddAppServicesConfiguration();

            // Assert
            var result = Assert.Single(services, portfolioAppService =>
                portfolioAppService.ServiceType == typeof(IPortfolioAppService) &&
                portfolioAppService.Lifetime == ServiceLifetime.Transient);
        }
        
        [Fact]
        public void IProductAppService_Registered_AsTransient_Successfully()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddAppServicesConfiguration();

            // Assert
            var result = Assert.Single(services, productAppService =>
                productAppService.ServiceType == typeof(IProductAppService) &&
                productAppService.Lifetime == ServiceLifetime.Transient);
        }
    }
}