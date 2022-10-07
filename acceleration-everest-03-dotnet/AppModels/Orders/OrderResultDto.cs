using AppModels.Portfolios;
using AppModels.Products;
using DomainModels.Enums;
using DomainModels.Models;
using System;

namespace AppModels.Orders;

public class OrderResultDto
{
    public OrderResultDto() { }

    public OrderResultDto(
        long id, 
        int quotes, 
        decimal netValue, 
        DateTime liquidatedAt, 
        OrderDirection direction,
        PortfolioResultForOthersDtos portfolio,
        ProductResultForOthersDtos product)
    {
        Id = id;
        Quotes = quotes;
        NetValue = netValue;
        LiquidatedAt = liquidatedAt;
        Direction = direction;
        Portfolio = portfolio;
        Product = product;
    }

    public long Id { get; set; }
    public int Quotes { get; set; }
    public decimal NetValue { get; set; }
    public DateTime LiquidatedAt { get; set; }
    public OrderDirection Direction { get; set; }
    public PortfolioResultForOthersDtos Portfolio { get; set; }
    public ProductResultForOthersDtos Product { get; set; }
}