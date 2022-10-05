using DomainModels.Enums;
using System;

namespace AppModels.Orders;

public class CreateOrderDto
{
    public CreateOrderDto(
        int quotes, 
        decimal netValue, 
        DateTime liquidatedAt, 
        OrderDirection direction, 
        long portifolioId, 
        long productId)
    {
        Quotes = quotes;
        NetValue = netValue;
        LiquidatedAt = liquidatedAt;
        Direction = direction;
        PortifolioId = portifolioId;
        ProductId = productId;
    }

    public int Quotes { get; set; }
    public decimal NetValue { get; set; }
    public DateTime LiquidatedAt { get; set; }
    public OrderDirection Direction { get; set; }
    public long PortifolioId { get; set; }
    public long ProductId { get; set; }
}