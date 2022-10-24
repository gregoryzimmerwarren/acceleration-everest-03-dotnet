using System.Collections.Generic;

namespace DomainModels.Models;

public class Portfolio : IEntity
{
    public Portfolio() { }

    public Portfolio(
        string name, 
        string description, 
        decimal totalBalance, 
        decimal accountBalance, 
        long customerId)
    {
        Name = name;
        Description = description;
        TotalBalance = totalBalance;
        AccountBalance = accountBalance;
        CustomerId = customerId;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal TotalBalance { get; set; }
    public decimal AccountBalance { get; set; }

    public long CustomerId { get; set; }
    public Customer Customer { get; set; }
    
    public ICollection<Product> Products { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<PortfolioProduct> PortfolioProducts { get; set; }
}