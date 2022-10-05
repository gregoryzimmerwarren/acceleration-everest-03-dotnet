using DomainModels.Models;

namespace AppModels.PortfoliosProducts;

public class PortfolioProductResultDto
{
    public PortfolioProductResultDto() { }

    public PortfolioProductResultDto(
        long id, 
        Portfolio portfolio, 
        Product product)
    {
        Id = id;
        Portfolio = portfolio;
        Product = product;
    }

    public long Id { get; set; }
    public Portfolio Portfolio { get; set; }
    public Product Product { get; set; }
}