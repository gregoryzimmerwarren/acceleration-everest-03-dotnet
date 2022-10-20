using AppModels.Portfolios;
using AppModels.Products;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppModels.Orders;

public class UpdateOrder
{
    public UpdateOrder(
        long id,
        int quotes,
        decimal netValue,
        string direction,
        bool wasExecuted,
        DateTime liquidatedAt,
        long portfolioId,
        long productId)
    {
        Id = id;
        Quotes = quotes;
        NetValue = netValue;
        Direction = direction;
        WasExecuted = wasExecuted;
        LiquidatedAt = liquidatedAt;
        PortfolioId = portfolioId;
        ProductId = productId;
        WasExecuted = false;
    }

    public long Id { get; set; }
    public int Quotes { get; set; }
    public decimal NetValue { get; set; }
    public string Direction { get; set; }
    public bool WasExecuted { get; set; }
    public long PortfolioId { get; set; }
    public long ProductId { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime LiquidatedAt { get; set; }
}