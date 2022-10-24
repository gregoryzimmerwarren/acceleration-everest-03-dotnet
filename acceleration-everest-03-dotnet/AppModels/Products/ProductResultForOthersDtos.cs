using AppModels.Enums;
using System;
using System.ComponentModel.DataAnnotations;

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
        Type = Enum.GetName(type);
    }

    public long Id { get; set; }
    public string Symbol { get; set; }
    public decimal UnitPrice { get; set; }
    public int DaysToExpire { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime ExpirationAt { get; set; }
    public string Type { get; set; }
}