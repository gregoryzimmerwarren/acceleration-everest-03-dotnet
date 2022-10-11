using DomainModels.Enums;
using System;
using System.Text.Json.Serialization;

namespace AppModels.Products;

public class CreateProductDto
{
    public CreateProductDto(
        string symbol, 
        decimal unitPrice,
        DateTime issuanceAt, 
        DateTime expirationAt,
        int type)
    {
        Symbol = symbol;
        UnitPrice = unitPrice;
        IssuanceAt = issuanceAt;
        ExpirationAt = expirationAt;
        Type = type;
    }

    public string Symbol { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime IssuanceAt { get; set; }
    public DateTime ExpirationAt { get; set; }
    public int Type { get; set; }
    [JsonIgnore]
    public int DaysToExpire { get; set; }
}