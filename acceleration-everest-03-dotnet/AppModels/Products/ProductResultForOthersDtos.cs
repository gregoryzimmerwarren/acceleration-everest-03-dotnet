using AppModels.Orders;
using DomainModels.Enums;
using System;
using System.Collections.Generic;

namespace AppModels.Products;

public class ProductResultForOthersDtos
{
    protected ProductResultForOthersDtos() { }

    public ProductResultForOthersDtos(
        long id,
        string symbol,
        decimal unitPrice,
        int daysToExpire,
        DateTime expirationAt,
        ProductType type)
    {
        Id = id;
        Symbol = symbol;
        UnitPrice = unitPrice;
        DaysToExpire = daysToExpire;
        ExpirationAt = expirationAt;
        Type = type;
    }

    public long Id { get; set; }
    public string Symbol { get; set; }
    public decimal UnitPrice { get; set; }
    public int DaysToExpire { get; set; }
    public DateTime ExpirationAt { get; set; }
    public ProductType Type { get; set; }
}