using System.Collections.Generic;

namespace DomainModels.Models;

public class Portfolio : IEntity
{
    public Portfolio(
        string name, 
        string description, 
        decimal totalBalance, 
        decimal accounBalance, 
        long customerId)
    {
        Name = name;
        Description = description;
        TotalBalance = totalBalance;
        AccounBalance = accounBalance;
        CustomerId = customerId;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal TotalBalance { get; set; }
    public decimal AccounBalance { get; set; }

    public long CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<Product> Products { get; set; }
    public List<Order> Orders { get; set; }
}
