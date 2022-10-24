using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(product => product.Id);

        builder.Property(product => product.Symbol)
            .HasColumnType("VARCHAR(50)")
            .IsRequired()
            .HasColumnName("Symbol");

        builder.Property(product => product.UnitPrice)
            .HasColumnType("DECIMAL(5,2)")
            .IsRequired()
            .HasColumnName("UnitPrice");

        builder.Property(product => product.DaysToExpire)
            .HasColumnType("INT")
            .IsRequired()
            .HasColumnName("DaysToExpire");

        builder.Property(product => product.IssuanceAt)
            .HasColumnType("DATE")
            .IsRequired()
            .HasColumnName("IssuanceAt");

        builder.Property(product => product.ExpirationAt)
            .HasColumnType("DATE")
            .IsRequired()
            .HasColumnName("ExpirationAt");

        builder.Property(product => product.Type)
            .HasColumnType("VARCHAR(11)")
            .IsRequired()
            .HasColumnName("Type");

        builder.HasMany(product => product.Portfolios)
            .WithMany(product => product.Products)
            .UsingEntity<PortfolioProduct>(
            joinTable => joinTable
                    .HasOne(portfolioProduct => portfolioProduct.Product)
                    .WithMany(product => product.PortfolioProducts)
                    .HasForeignKey(portfolioProduct => portfolioProduct.ProductId));
    }
}