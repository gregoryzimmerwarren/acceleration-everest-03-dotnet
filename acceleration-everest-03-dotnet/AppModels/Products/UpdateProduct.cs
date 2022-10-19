using System;
using System.ComponentModel.DataAnnotations;

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
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime ExpirationAt { get; set; }
}