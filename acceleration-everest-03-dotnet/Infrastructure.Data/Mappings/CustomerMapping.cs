using DomainModels;
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
            .HasColumnType("VARCHAR(50)")
            .IsRequired()
            .HasColumnName("full_name");
        
        builder.Property(customer => customer.Email)
            .HasColumnType("VARCHAR(50)")
            .IsRequired()
            .HasColumnName("email");

        builder.Property(customer => customer.Cpf)
            .HasColumnType("VARCHAR(11)")
            .IsRequired()
            .HasColumnName("cpf");

        builder.Property(customer => customer.Cellphone)
            .HasColumnType("VARCHAR(11)")
            .IsRequired()
            .HasColumnName("cellphone");

        builder.Property(customer => customer.Country)
            .HasColumnType("VARCHAR(50)")
            .IsRequired()
            .HasColumnName("country");

        builder.Property(customer => customer.City)
            .HasColumnType("VARCHAR(50)")
            .IsRequired()
            .HasColumnName("city");

        builder.Property(customer => customer.Address)
            .HasColumnType("VARCHAR(100)")
            .IsRequired()
            .HasColumnName("address");

        builder.Property(customer => customer.PostalCode)
            .HasColumnType("VARCHAR(8)")
            .IsRequired()
            .HasColumnName("postal_code");

        builder.Property(customer => customer.Number)
            .HasColumnType("INT")
            .IsRequired()
            .HasColumnName("number");

        builder.Property(customer => customer.EmailSms)
            .HasColumnType("BIT")
            .IsRequired()
            .HasColumnName("email_sms");

        builder.Property(customer => customer.Whatsapp)
            .HasColumnType("BIT")
            .IsRequired()
            .HasColumnName("whatsapp");

        builder.Property(customer => customer.DateOfBirth)
            .HasColumnType("DATE")
            .IsRequired()
            .HasColumnName("date_of_birth");
    }
}