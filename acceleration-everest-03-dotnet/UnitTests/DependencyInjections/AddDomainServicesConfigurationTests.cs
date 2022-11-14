using DomainServices.DependencyInjections;
using DomainServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.DependencyInjections
{
    public class AddDomainServicesConfigurationTests
    {
        [Fact]
        public void ICustomerBankInfoService_Registered_AsTransient_Successfully()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddDomainServicesConfiguration();

            // Assert
            var result = Assert.Single(services, customerBankInfoService =>
                customerBankInfoService.ServiceType == typeof(ICustomerBankInfoService) &&
                customerBankInfoService.Lifetime == ServiceLifetime.Transient);
        }

        [Fact]
        public void ICustomerService_Registered_AsTransient_Successfully()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddDomainServicesConfiguration();

            // Assert
            var result = Assert.Single(services, customerService =>
                customerService.ServiceType == typeof(ICustomerService) &&
                customerService.Lifetime == ServiceLifetime.Transient);
        }

        [Fact]
        public void IOrderService_Registered_AsTransient_Successfully()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddDomainServicesConfiguration();

            // Assert
            var result = Assert.Single(services, orderService =>
                orderService.ServiceType == typeof(IOrderService) &&
                orderService.Lifetime == ServiceLifetime.Transient);
        }

        [Fact]
        public void IPortfolioService_Registered_AsTransient_Successfully()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddDomainServicesConfiguration();

            // Assert
            var result = Assert.Single(services, portfolioService =>
                portfolioService.ServiceType == typeof(IPortfolioService) &&
                portfolioService.Lifetime == ServiceLifetime.Transient);
        }

        [Fact]
        public void IPortfolioProductService_Registered_AsTransient_Successfully()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddDomainServicesConfiguration();

            // Assert
            var result = Assert.Single(services, portfolioProductService =>
                portfolioProductService.ServiceType == typeof(IPortfolioProductService) &&
                portfolioProductService.Lifetime == ServiceLifetime.Transient);
        }

        [Fact]
        public void IProductService_Registered_AsTransient_Successfully()
        {
            // Arrange
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder().Build();

            // Act
            services.AddDomainServicesConfiguration();

            // Assert
            var result = Assert.Single(services, productService =>
                productService.ServiceType == typeof(IProductService) &&
                productService.Lifetime == ServiceLifetime.Transient);
        }
    }
}
