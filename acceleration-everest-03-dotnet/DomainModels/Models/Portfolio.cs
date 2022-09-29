using System.Collections.Generic;

namespace DomainModels.Models
{
    public class Portfolio : IEntity
    {
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
}
