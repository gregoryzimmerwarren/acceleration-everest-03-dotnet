using AppModels.Customers;
using AppModels.Orders;
using AppModels.Products;
using System.Collections.Generic;

namespace AppModels.Portfolios;

public class PortfolioResultDto
{
    public PortfolioResultDto() { }

    public PortfolioResultDto(
        long id, 
        string name, 
        string description, 
        decimal totalBalance, 
        decimal accountBalance,
        CustomerResultForOtherDtos customer, 
        List<ProductResultForOthersDtos> products, 
        List<OrderResultOtherDtos> orders)
    {
        Id = id;
        Name = name;
        Description = description;
        TotalBalance = totalBalance;
        AccountBalance = accountBalance;
        Customer = customer;
        Products = products;
        Orders = orders;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal TotalBalance { get; set; }
    public decimal AccountBalance { get; set; }
    public CustomerResultForOtherDtos Customer { get; set; }
    public List<ProductResultForOthersDtos> Products { get; set; }
    public List<OrderResultOtherDtos> Orders { get; set; }
}