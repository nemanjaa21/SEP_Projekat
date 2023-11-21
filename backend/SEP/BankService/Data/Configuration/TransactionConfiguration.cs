using BankService.Enums;
using BankService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankService.Data.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.MerchantOrderId).IsRequired();
            builder.Property(x => x.MerchantTimestamp).IsRequired();
            builder.Property(x => x.AcquirerOrderId).IsRequired();
            builder.Property(x => x.AcquirerTimestamp).IsRequired();
            builder.Property(x => x.IssuerOrderId).IsRequired();
            builder.Property(x => x.IssuerTimestamp).IsRequired();
            builder.Property(x => x.Status).HasConversion(new EnumToStringConverter<Status>()).IsRequired();

            builder.Property(x => x.PaymentId).IsRequired();
            builder.Property(x => x.AcquirerAccountNumber).IsRequired();
            builder.Property(x => x.IssuerAccountNumber).IsRequired();
            builder.Property(x => x.Merchant_Id);

            builder.HasOne(x => x.Merchant)
                   .WithMany(t => t.Transactions)
                   .HasForeignKey(x => x.IdMerchant);

            builder.HasOne(x => x.User)
                   .WithMany(t => t.Transactions)
                   .HasForeignKey(x => x.IdUser);
        }
    }
}
