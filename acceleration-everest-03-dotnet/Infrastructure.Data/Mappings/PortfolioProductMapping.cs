using DomainModels.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Mappings
{
    public class PortfolioProductMapping : IEntityTypeConfiguration<PortfolioProduct>
    {
        public void Configure(EntityTypeBuilder<PortfolioProduct> builder)
        {
            builder.HasKey(portfolioProduct => portfolioProduct.Id);

            builder.Property(portfolioProduct => portfolioProduct.Id)
                .ValueGeneratedOnAdd();

            builder.Property(portfolioProduct => portfolioProduct.PortfolioId)
                .HasColumnType("BIGINT")
                .IsRequired()
                .HasColumnName("PortfolioId");
                        
            builder.HasOne(portfolioProduct => portfolioProduct.Portfolio)
                .WithMany(portfolio => portfolio.PortfolioProducts)
                .HasForeignKey(portfolioProduct => portfolioProduct.PortfolioId);

            builder.Property(portfolioProduct => portfolioProduct.ProductId)
                .HasColumnType("BIGINT")
                .IsRequired()
                .HasColumnName("ProductId");

            builder.HasOne(portfolioProduct => portfolioProduct.Product)
                .WithMany(product => product.PortfolioProducts)
                .HasForeignKey(portfolioProduct => portfolioProduct.ProductId);
        }
    }
}