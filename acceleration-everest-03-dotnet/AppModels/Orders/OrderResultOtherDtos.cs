using AppModels.Enums;
using AppModels.Products;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppModels.Orders;

public class OrderResultOtherDtos
{
    protected OrderResultOtherDtos() { }

    public OrderResultOtherDtos(
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
        Direction = Enum.GetName(direction);
        WasExecuted = wasExecuted;
        Product = product;

    }

    public long Id { get; set; }
    public int Quotes { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal NetValue { get; set; }
    public string Direction { get; set; }
    public bool WasExecuted { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime LiquidatedAt { get; set; }
    public ProductResult Product { get; set; }
}