using Infrastructure.CrossCutting.Enums;
using System;

namespace DomainModels.Models;

public class Order : IEntity
{
    public Order(
        int quotes, 
        decimal unitPrice,
        DateTime liquidatedAt, 
        OrderDirection direction,
        bool wasExecuted,
        long portfolioId, 
        long productId)
    {
        Quotes = quotes;
        UnitPrice = unitPrice;
        LiquidatedAt = liquidatedAt;
        Direction = direction;
        WasExecuted = wasExecuted;
        PortfolioId = portfolioId;
        ProductId = productId;
    }

    public long Id { get; set; }
    public int Quotes { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal NetValue { get; set; }
    public DateTime LiquidatedAt { get; set; }
    public bool WasExecuted { get; set; }
    public OrderDirection Direction { get; set; }
    
    public long PortfolioId { get; set; }
    public Portfolio Portfolio { get; set; }
    
    public long ProductId { get; set; }
    public Product Product { get; set; }
}