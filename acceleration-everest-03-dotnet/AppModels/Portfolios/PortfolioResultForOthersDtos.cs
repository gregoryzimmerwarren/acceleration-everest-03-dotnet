using AppModels.Orders;
using AppModels.Products;
using System.Collections.Generic;

namespace AppModels.Portfolios;

public class PortfolioResultForOthersDtos
{
    public PortfolioResultForOthersDtos() { }

    public PortfolioResultForOthersDtos(
        long id,
        string name,
        string description,
        decimal totalBalance,
        decimal accountBalance,
        ICollection<ProductResultForOthersDtos> products,
        List<OrderResultOtherDtos> orders)
    {
        Id = id;
        Name = name;
        Description = description;
        TotalBalance = totalBalance;
        AccountBalance = accountBalance;
        Products = products;
        Orders = orders;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal TotalBalance { get; set; }
    public decimal AccountBalance { get; set; }
    public ICollection<ProductResultForOthersDtos> Products { get; set; }
    public List<OrderResultOtherDtos> Orders { get; set; }
}