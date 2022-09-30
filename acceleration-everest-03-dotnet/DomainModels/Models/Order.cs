using DomainModels.Enums;
using System;

namespace DomainModels.Models;

public class Order : IEntity
{
    public Order(
        int quotes, 
        DateTime liquidatedAt, 
        OrderDirection direction, 
        long portifolioId, 
        long productId)
    {
        Quotes = quotes;
        LiquidatedAt = liquidatedAt;
        Direction = direction;
        PortifolioId = portifolioId;
        ProductId = productId;
    }

    public long Id { get; set; }
    public int Quotes { get; set; }
    public decimal NetValue { get; set; }
    public DateTime LiquidatedAt { get; set; }

    public OrderDirection Direction { get; set; }
    public long PortifolioId { get; set; }
    public Portfolio Portfolio { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; }
}
