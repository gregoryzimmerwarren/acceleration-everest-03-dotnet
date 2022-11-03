using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings;

public class PortfolioMapping : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.ToTable("Portfolios");

        builder.HasKey(portfolio => portfolio.Id);

        builder.Property(portfolio => portfolio.Id)
            .ValueGeneratedOnAdd();

        builder.Property(portfolio => portfolio.Name)
            .HasColumnType("VARCHAR(50)")
            .IsRequired()
            .HasColumnName("Name");

        builder.Property(portfolio => portfolio.Description)
            .HasColumnType("VARCHAR(250)")
            .IsRequired()
            .HasColumnName("Description");

        builder.Property(portfolio => portfolio.TotalBalance)
            .HasColumnType("DECIMAL(14,2)")
            .IsRequired()
            .HasColumnName("TotalBalance");

        builder.Property(portfolio => portfolio.AccountBalance)
            .HasColumnType("DECIMAL(14,2)")
            .IsRequired()
            .HasColumnName("AccountBalance");

        builder.Property(portfolio => portfolio.CustomerId)
            .HasColumnType("BIGINT")
            .IsRequired()
            .HasColumnName("CustomerId");

        builder.HasOne(portfolio => portfolio.Customer)
            .WithMany(portfolio => portfolio.Portfolios)
            .HasForeignKey(portfolio => portfolio.CustomerId);
    }
}