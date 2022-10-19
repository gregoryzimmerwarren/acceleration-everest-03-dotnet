using DomainModels.Enums;
using System;
using System.ComponentModel.DataAnnotations;

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
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime LiquidatedAt { get; set; }
    public string Direction { get; set; }
}