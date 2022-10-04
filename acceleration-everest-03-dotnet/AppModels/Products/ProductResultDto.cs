using DomainModels.Enums;
using DomainModels.Models;
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
        ICollection<Portfolio> portfolios, 
        List<Order> orders, 
        List<PortfolioProduct> portfolioProducts)
    {
        Id = id;
        Symbol = symbol;
        UnitPrice = unitPrice;
        DaysToExpire = daysToExpire;
        ExpirationAt = expirationAt;
        Type = type;
        Portfolios = portfolios;
        Orders = orders;
        PortfolioProducts = portfolioProducts;
    }

    public long Id { get; set; }
    public string Symbol { get; set; }
    public decimal UnitPrice { get; set; }
    public int DaysToExpire { get; set; }
    public DateTime ExpirationAt { get; set; }
    public ProductType Type { get; set; }
    public ICollection<Portfolio> Portfolios { get; set; }
    public List<Order> Orders { get; set; }
    public List<PortfolioProduct> PortfolioProducts { get; set; }
}