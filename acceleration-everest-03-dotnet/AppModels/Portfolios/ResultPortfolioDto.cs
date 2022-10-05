using DomainModels.Models;
using System.Collections.Generic;

namespace AppModels.Portfolios;

public class ResultPortfolioDto
{
    public ResultPortfolioDto() { }

    public ResultPortfolioDto(
        long id, 
        string name, 
        string description, 
        decimal totalBalance, 
        decimal accountBalance, 
        Customer customer, 
        ICollection<Product> products, 
        List<Order> orders, 
        List<PortfolioProduct> portfolioProducts)
    {
        Id = id;
        Name = name;
        Description = description;
        TotalBalance = totalBalance;
        AccountBalance = accountBalance;
        Customer = customer;
        Products = products;
        Orders = orders;
        PortfolioProducts = portfolioProducts;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal TotalBalance { get; set; }
    public decimal AccountBalance { get; set; }
    public Customer Customer { get; set; }
    public ICollection<Product> Products { get; set; }
    public List<Order> Orders { get; set; }
    public List<PortfolioProduct> PortfolioProducts { get; set; }
}