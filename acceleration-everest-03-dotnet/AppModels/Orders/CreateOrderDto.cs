using DomainModels.Enums;
using System;
using System.Text.Json.Serialization;

namespace AppModels.Orders;

public class CreateOrderDto
{
    public CreateOrderDto(
        int quotes, 
        DateTime liquidatedAt, 
        long portfolioId, 
        long productId)
    {
        Quotes = quotes;
        LiquidatedAt = liquidatedAt;
        PortfolioId = portfolioId;
        ProductId = productId;
    }
    
    public int Quotes { get; set; }
    public DateTime LiquidatedAt { get; set; }
    public long PortfolioId { get; set; }
    public long ProductId { get; set; }
    [JsonIgnore]
    public OrderDirection Direction { get; set; }
}