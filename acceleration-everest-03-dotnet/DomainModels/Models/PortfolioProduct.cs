namespace DomainModels.Models
{
    public class PortfolioProduct : IEntity
    {
        public PortfolioProduct(
            long portfolioId, 
            long productId)
        {
            PortfolioId = portfolioId;
            ProductId = productId;
        }

        public long Id { get; set; }
        public long PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
