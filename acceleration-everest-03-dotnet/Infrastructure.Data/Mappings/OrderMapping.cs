using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings;

public class OrderMapping : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(order => order.Id);

        builder.Property(order => order.Id)
            .ValueGeneratedOnAdd();

        builder.Property(order => order.Quotes)
            .HasColumnType("INT")
            .IsRequired()
            .HasColumnName("Quotes");


        builder.Property(order => order.UnitPrice)
            .HasColumnType("DECIMAL(5,2)")
            .IsRequired()
            .HasColumnName("UnitPrice");

        builder.Property(order => order.NetValue)
            .HasColumnType("DECIMAL(14,2)")
            .IsRequired()
            .HasColumnName("NetValue");

        builder.Property(order => order.LiquidatedAt)
            .HasColumnType("DATE")
            .IsRequired()
            .HasColumnName("LiquidatedAt");

        builder.Property(order => order.Direction)
            .HasColumnType("VARCHAR(4)")
            .IsRequired()
            .HasColumnName("Direction");

        builder.Property(order => order.WasExecuted)
            .HasColumnType("BIT")
            .IsRequired()
            .HasColumnName("WasExecuted");

        builder.Property(order => order.PortfolioId)
            .HasColumnType("BIGINT")
            .IsRequired()
            .HasColumnName("PortfolioId");

        builder.HasOne(order => order.Portfolio)
            .WithMany(order => order.Orders)
            .HasForeignKey(order => order.PortfolioId);

        builder.Property(order => order.ProductId)
            .HasColumnType("BIGINT")
            .IsRequired()
            .HasColumnName("ProductId");
    }
}