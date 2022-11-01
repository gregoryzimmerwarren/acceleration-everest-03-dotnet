using DomainServices.Services;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data;
using Moq;

namespace DomainServices.Tests.Services;

public class ProductServiceTests
{
    private readonly Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>> _mockRepositoryFactory;
    private readonly Mock<IUnitOfWork<WarrenEverestDotnetDbContext>> _mockUnitOfWork;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork<WarrenEverestDotnetDbContext>>();
        _mockRepositoryFactory = new Mock<IRepositoryFactory<WarrenEverestDotnetDbContext>>();
        _productService = new ProductService(_mockUnitOfWork.Object, _mockRepositoryFactory.Object);
    }


}