namespace DomainModels.Models
{
    public class CustomerBankInfo : IEntity
    {
        public long Id { get; set; }
        public decimal AccountBalance { get; set; }

        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
