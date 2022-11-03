using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppModels.Products;

public class UpdateProduct
{
    public UpdateProduct(
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
        DaysToExpire = (expirationAt.Subtract(issuanceAt)).Days;
    }

    public string Symbol { get; set; }
    public decimal UnitPrice { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime IssuanceAt { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime ExpirationAt { get; set; }
    public int Type { get; set; }
    [JsonIgnore]
    public int DaysToExpire { get; set; }
}