using AppModels.Orders;
using AppModels.Portfolios;
using DomainModels.Enums;
using System;
using System.Collections.Generic;

namespace AppModels.Products;

public class ProductResultDto
{
    protected ProductResultDto() { }

    public ProductResultDto(
        long id, 
        string symbol, 
        decimal unitPrice, 
        int daysToExpire, 
        DateTime expirationAt, 
        ProductType type, 
        List<PortfolioResultForOthersDtos> portfolios, 
        List<OrderResultOtherDtos> orders)
    {
        Id = id;
        Symbol = symbol;
        UnitPrice = unitPrice;
        DaysToExpire = daysToExpire;
        ExpirationAt = expirationAt;
        Type = type;
        Portfolios = portfolios;
        Orders = orders;
    }

    public long Id { get; set; }
    public string Symbol { get; set; }
    public decimal UnitPrice { get; set; }
    public int DaysToExpire { get; set; }
    public DateTime ExpirationAt { get; set; }
    public ProductType Type { get; set; }
    public List<PortfolioResultForOthersDtos> Portfolios { get; set; }
    public List<OrderResultOtherDtos> Orders { get; set; }
}