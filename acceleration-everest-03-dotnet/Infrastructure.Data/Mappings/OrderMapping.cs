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

        builder.Property(order => order.Quotes)
            .HasColumnType("INT")
            .IsRequired()
            .HasColumnName("Quotes");

        builder.Property(order => order.NetValue)
            .HasColumnType("DECIMAL(14,2)")
            .IsRequired()
            .HasColumnName("NetValue");

        builder.Property(order => order.LiquidatedAt)
            .HasColumnType("DATE")
            .IsRequired()
            .HasColumnName("LiquidatedAt");

        builder.Property(order => order.Direction)
            .HasColumnType("BIT")
            .IsRequired()
            .HasColumnName("Direction");

        builder.Property(order => order.PortifolioId)
            .HasColumnType("INT")
            .IsRequired()
            .HasColumnName("PortifolioId");

        builder.HasOne(order => order.Portfolio)
            .WithMany(order => order.Orders)
            .HasForeignKey(order => order.PortifolioId);

        builder.Property(order => order.ProductId)
            .HasColumnType("INT")
            .IsRequired()
            .HasColumnName("ProductId");

        builder.HasOne(order => order.Product)
            .WithMany(order => order.Orders)
            .HasForeignKey(order => order.ProductId);
    }
}