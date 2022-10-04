using System;

namespace AppModels.Products;

public class UpdateProductDto
{
    public UpdateProductDto(
    string symbol,
    decimal unitPrice,
    DateTime issuanceAt,
    DateTime expirationAt)
    {
        Symbol = symbol;
        UnitPrice = unitPrice;
        DaysToExpire = (expirationAt.Subtract(issuanceAt)).Days;
        IssuanceAt = issuanceAt;
        ExpirationAt = expirationAt;
    }

    public string Symbol { get; set; }
    public decimal UnitPrice { get; set; }
    public int DaysToExpire { get; set; }
    public DateTime IssuanceAt { get; set; }
    public DateTime ExpirationAt { get; set; }
}