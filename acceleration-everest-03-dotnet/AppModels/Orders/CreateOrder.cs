using Infrastructure.CrossCutting.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppModels.Orders;

public class CreateOrder
{
    public CreateOrder(
        int quotes,
        DateTime liquidatedAt, 
        long portfolioId, 
        long productId)
    {
        Quotes = quotes;      
        LiquidatedAt = liquidatedAt;
        PortfolioId = portfolioId;
        ProductId = productId;
        WasExecuted = false;
    }
    
    public int Quotes { get; set; }
    [JsonIgnore]
    public decimal UnitPrice { get; set; }
    [JsonIgnore]
    public decimal NetValue { get; set; }
    public long PortfolioId { get; set; }
    public long ProductId { get; set; }

    [JsonIgnore]
    public bool WasExecuted { get; set; }

    [JsonIgnore]
    public OrderDirection Direction { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime LiquidatedAt { get; set; }
}