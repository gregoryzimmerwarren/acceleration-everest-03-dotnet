using DomainModels.Enums;
using DomainModels.Models;
using System;

namespace AppModels.Orders;

public class ResultOrderDto
{
    public ResultOrderDto() { }

    public ResultOrderDto(
        long id, 
        int quotes, 
        decimal netValue, 
        DateTime liquidatedAt, 
        OrderDirection direction, 
        Portfolio portfolio, 
        Product product)
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
    public Portfolio Portfolio { get; set; }
    public Product Product { get; set; }
}