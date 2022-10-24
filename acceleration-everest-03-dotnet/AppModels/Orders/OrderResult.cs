using AppModels.Enums;
using AppModels.Portfolios;
using AppModels.Products;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppModels.Orders;

public class OrderResult
{
    protected OrderResult() { }

    public OrderResult(
        long id, 
        int quotes, 
        decimal netValue, 
        DateTime liquidatedAt, 
        OrderDirection direction,
        bool wasExecuted,
        PortfolioResultForOthersDtos portfolio,
        ProductResultForOthersDtos product)
    {
        Id = id;
        Quotes = quotes;
        NetValue = netValue;
        LiquidatedAt = liquidatedAt;
        Direction = Enum.GetName(direction);
        WasExecuted = wasExecuted;
        Portfolio = portfolio;
        Product = product;
    }

    public long Id { get; set; }
    public int Quotes { get; set; }
    public decimal NetValue { get; set; }
    public string Direction { get; set; }
    public bool WasExecuted { get; set; }
    public PortfolioResultForOthersDtos Portfolio { get; set; }
    public ProductResultForOthersDtos Product { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime LiquidatedAt { get; set; }
}