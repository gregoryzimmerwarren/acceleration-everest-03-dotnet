using System;

namespace AppModels.Products;

public class UpdateProduct
{
    public UpdateProduct(
    string symbol,
    decimal unitPrice,
    DateTime expirationAt)
    {
        Symbol = symbol;
        UnitPrice = unitPrice;
        ExpirationAt = expirationAt;
    }

    public string Symbol { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime ExpirationAt { get; set; }
}