using DomainModels.Enums;
using System;

namespace AppModels.Orders;

public class CreateOrderDto
{
    public CreateOrderDto(
        int quotes, 
        decimal netValue, 
        DateTime liquidatedAt, 
        long portfolioId, 
        long productId)
    {
        Quotes = quotes;
        NetValue = netValue;
        LiquidatedAt = liquidatedAt;
        PortfolioId = portfolioId;
        ProductId = productId;
    }
    
    public int Quotes { get; set; }
    public decimal NetValue { get; set; }
    public DateTime LiquidatedAt { get; set; }
    public OrderDirection Direction { get; set; }
    public long PortfolioId { get; set; }
    public long ProductId { get; set; }
}