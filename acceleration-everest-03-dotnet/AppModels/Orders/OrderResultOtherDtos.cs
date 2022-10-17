using DomainModels.Enums;
using System;

namespace AppModels.Orders;

public class OrderResultOtherDtos
{
    protected OrderResultOtherDtos() { }

    public OrderResultOtherDtos(
        long id,
        int quotes,
        decimal netValue,
        DateTime liquidatedAt,
        OrderDirection direction)
    {
        Id = id;
        Quotes = quotes;
        NetValue = netValue;
        LiquidatedAt = liquidatedAt;
        Direction = Enum.GetName(direction);
    }

    public long Id { get; set; }
    public int Quotes { get; set; }
    public decimal NetValue { get; set; }
    public DateTime LiquidatedAt { get; set; }
    public string Direction { get; set; }
}