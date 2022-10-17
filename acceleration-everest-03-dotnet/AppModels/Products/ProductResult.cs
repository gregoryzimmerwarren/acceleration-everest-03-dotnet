using AppModels.Orders;
using AppModels.Portfolios;
using DomainModels.Enums;
using System;
using System.Collections.Generic;

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
    public DateTime ExpirationAt { get; set; }
    public string Type { get; set; }
    public IEnumerable<PortfolioResultForOthersDtos> Portfolios { get; set; }
    public IEnumerable<OrderResultOtherDtos> Orders { get; set; }
}