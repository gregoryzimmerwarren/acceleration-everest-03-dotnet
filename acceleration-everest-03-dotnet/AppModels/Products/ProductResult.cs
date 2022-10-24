using AppModels.Enums;
using AppModels.Orders;
using AppModels.Portfolios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.Products;

public class ProductResult
{
    protected ProductResult() { }

    public ProductResult(
        long id, 
        string symbol, 
        decimal unitPrice, 
        int daysToExpire, 
        DateTime expirationAt, 
        ProductType type,
        IEnumerable<PortfolioResultForOthersDtos> portfolios,
        IEnumerable<OrderResultOtherDtos> orders)
    {
        Id = id;
        Symbol = symbol;
        UnitPrice = unitPrice;
        DaysToExpire = daysToExpire;
        ExpirationAt = expirationAt;
        Type = Enum.GetName(type);
        Portfolios = portfolios;
        Orders = orders;
    }

    public long Id { get; set; }
    public string Symbol { get; set; }
    public decimal UnitPrice { get; set; }
    public int DaysToExpire { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime ExpirationAt { get; set; }
    public string Type { get; set; }
    public IEnumerable<PortfolioResultForOthersDtos> Portfolios { get; set; }
    public IEnumerable<OrderResultOtherDtos> Orders { get; set; }
}