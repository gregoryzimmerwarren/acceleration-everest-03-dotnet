using AppModels.PortfoliosProducts;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Services;

public class PortfolioProductAppService : IPortfolioProductAppService
{
    private readonly IPortfolioProductService _portfolioProductService;
    private readonly IMapper _mapper;

    public PortfolioProductAppService(
        IPortfolioProductService portfolioProductService,
        IMapper mapper)
    {
        _portfolioProductService = portfolioProductService ?? throw new System.ArgumentNullException(nameof(portfolioProductService));
        _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    public long Create(CreatePortfolioProduct createPortfolioProductDto)
    {
        var mappedPortfolioProduct = _mapper.Map<PortfolioProduct>(createPortfolioProductDto);

        return _portfolioProductService.Create(mappedPortfolioProduct);
    }

    public async Task DeleteAsync(long portfolioId, long ProductId)
    {
        await _portfolioProductService.DeleteAsync(portfolioId, ProductId).ConfigureAwait(false);
    }

    public async Task<IEnumerable<PortfolioProductResult>> GetAllPortfolioProductAsync()
    {
        var portfoliosProducts = await _portfolioProductService.GetAllPortfolioProductAsync().ConfigureAwait(false);

        return _mapper.Map<IEnumerable<PortfolioProductResult>>(portfoliosProducts);
    }

    public async Task<PortfolioProductResult> GetPortfolioProductByIdsAsync(long portfolioId, long ProductId)
    {
        var portfolioProduct = await _portfolioProductService.GetPortfolioProductByIdsAsync(portfolioId, ProductId).ConfigureAwait(false);

        return _mapper.Map<PortfolioProductResult>(portfolioProduct);
    }

    public async Task<IEnumerable<PortfolioProductResult>> GetPortfolioProductByProductIdAsync(long productId)
    {
        var portfoliosProducts = await _portfolioProductService.GetPortfolioProductByProductIdAsync(productId).ConfigureAwait(false);

        return _mapper.Map<IEnumerable<PortfolioProductResult>>(portfoliosProducts);
    }

    public async Task<IEnumerable<PortfolioProductResult>> GetPortfolioProductByPortfolioIdAsync(long portfolioId)
    {
        var portfoliosProducts = await _portfolioProductService.GetPortfolioProductByPortfolioIdAsync(portfolioId).ConfigureAwait(false);

        return _mapper.Map<IEnumerable<PortfolioProductResult>>(portfoliosProducts);
    }
}
