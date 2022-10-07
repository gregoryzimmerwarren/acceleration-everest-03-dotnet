using DomainModels.Enums;
using System;

namespace AppModels.Products;

public class CreateProductDto
{
    public CreateProductDto(
        string symbol, 
        decimal unitPrice,
        DateTime issuanceAt, 
        DateTime expirationAt,
        ProductType type)
    {
        Symbol = symbol;
        UnitPrice = unitPrice;
        DaysToExpire = (expirationAt.Subtract(issuanceAt)).Days;
        IssuanceAt = issuanceAt;
        ExpirationAt = expirationAt;
        Type = type;
    }

    public string Symbol { get; set; }
    public decimal UnitPrice { get; set; }
    public int DaysToExpire { get; set; }
    public DateTime IssuanceAt { get; set; }
    public DateTime ExpirationAt { get; set; }
    public ProductType Type { get; set; }
}