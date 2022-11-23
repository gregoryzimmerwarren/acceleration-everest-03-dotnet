using AppModels.Enums;
using AppModels.Products;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppModels.Orders;

public class OrderResultForOtherDtos
{
    protected OrderResultForOtherDtos() { }

    public OrderResultForOtherDtos(
        long id,
        int quotes,
        decimal unitPrice,
        decimal netValue,
        DateTime liquidatedAt,
        OrderDirection direction,
        bool wasExecuted,
        ProductResult product)
    {
        Id = id;
        Quotes = quotes;
        UnitPrice = unitPrice;
        NetValue = netValue;
        LiquidatedAt = liquidatedAt;
        Direction = direction;
        WasExecuted = wasExecuted;
        Product = product;

    }

    public long Id { get; set; }
    public int Quotes { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal NetValue { get; set; }
    public OrderDirection Direction { get; set; }
    public bool WasExecuted { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime LiquidatedAt { get; set; }
    public ProductResult Product { get; set; }
}