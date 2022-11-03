using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings;

public class CustomerBankInfoMapping : IEntityTypeConfiguration<CustomerBankInfo>
{
    public void Configure(EntityTypeBuilder<CustomerBankInfo> builder)
    {
        builder.ToTable("CustomersBankInfo");
        
        builder.HasKey(customerBankInfo => customerBankInfo.Id);

        builder.Property(customerBankInfo => customerBankInfo.Id)
            .ValueGeneratedOnAdd();

        builder.Property(customerBankInfo => customerBankInfo.AccountBalance)
            .HasColumnType("DECIMAL(14,2)")
            .IsRequired()
            .HasColumnName("AccountBalance");

        builder.Property(customerBankInfo => customerBankInfo.CustomerId)
            .HasColumnType("BIGINT")
            .IsRequired()
            .HasColumnName("CustomerId");
    }
}