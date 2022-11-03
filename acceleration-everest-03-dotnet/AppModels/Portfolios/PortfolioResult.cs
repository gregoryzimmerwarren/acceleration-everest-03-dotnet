using AppModels.Customers;
using AppModels.Orders;
using AppModels.PortfoliosProducts;
using AppModels.Products;
using System.Collections.Generic;

namespace AppModels.Portfolios;

public class PortfolioResult
{
    protected PortfolioResult() { }

    public PortfolioResult(
        long id, 
        string name, 
        string description, 
        decimal totalBalance, 
        decimal accountBalance,
        CustomerResultForOtherDtos customer,
        IEnumerable<OrderResultOtherDtos> orders)
    {
        Id = id;
        Name = name;
        Description = description;
        TotalBalance = totalBalance;
        AccountBalance = accountBalance;
        Customer = customer;
        Orders = orders;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal TotalBalance { get; set; }
    public decimal AccountBalance { get; set; }
    public CustomerResultForOtherDtos Customer { get; set; }
    public IEnumerable<OrderResultOtherDtos> Orders { get; set; }
}