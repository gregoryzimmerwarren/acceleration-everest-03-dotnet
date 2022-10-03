using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings;

public class CustomerMapping : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(customer => customer.Id);

        builder.Property(customer => customer.FullName)
            .HasColumnType("VARCHAR(250)")
            .IsRequired()
            .HasColumnName("FullName");
        
        builder.Property(customer => customer.Email)
            .HasColumnType("VARCHAR(50)")
            .IsRequired()
            .HasColumnName("Email");

        builder.Property(customer => customer.Cpf)
            .HasColumnType("VARCHAR(11)")
            .IsRequired()
            .HasColumnName("Cpf");

        builder.Property(customer => customer.Cellphone)
            .HasColumnType("VARCHAR(11)")
            .IsRequired()
            .HasColumnName("Cellphone");

        builder.Property(customer => customer.Country)
            .HasColumnType("VARCHAR(50)")
            .IsRequired()
            .HasColumnName("Country");

        builder.Property(customer => customer.City)
            .HasColumnType("VARCHAR(50)")
            .IsRequired()
            .HasColumnName("City");

        builder.Property(customer => customer.Address)
            .HasColumnType("VARCHAR(100)")
            .IsRequired()
            .HasColumnName("Address");

        builder.Property(customer => customer.PostalCode)
            .HasColumnType("VARCHAR(8)")
            .IsRequired()
            .HasColumnName("PostalCode");

        builder.Property(customer => customer.Number)
            .HasColumnType("INT")
            .IsRequired()
            .HasColumnName("Number");

        builder.Property(customer => customer.EmailSms)
            .HasColumnType("BIT")
            .IsRequired()
            .HasColumnName("EmailSms");

        builder.Property(customer => customer.Whatsapp)
            .HasColumnType("BIT")
            .IsRequired()
            .HasColumnName("Whatsapp");

        builder.Property(customer => customer.DateOfBirth)
            .HasColumnType("DATE")
            .IsRequired()
            .HasColumnName("DateOfBirth");

        builder.HasOne(customer => customer.CustomerBankInfo)
            .WithOne(customerBankInfo => customerBankInfo.Customer)
            .HasForeignKey<CustomerBankInfo>(customerBankInfo => customerBankInfo.CustomerId);
    }
}