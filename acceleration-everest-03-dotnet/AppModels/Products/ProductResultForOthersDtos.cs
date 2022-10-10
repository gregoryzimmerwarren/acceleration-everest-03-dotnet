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
        Type = GetEnumName(type);
    }

    public long Id { get; set; }
    public string Symbol { get; set; }
    public decimal UnitPrice { get; set; }
    public int DaysToExpire { get; set; }
    public DateTime ExpirationAt { get; set; }
    public string Type { get; set; }

    private static string GetEnumName(ProductType direction)
    {
        if (direction == ProductType.FixedIncome)
            return "FixedIncome";

        if (direction == ProductType.Trade)
            return "Trade";

        if (direction == ProductType.Funds)
            return "Funds";

        if (direction == ProductType.Fii)
            return "Fii";

        return "Crypto";
    }
}