using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using BitcoinPaymentService.Models;
using BitcoinPaymentService.Enums;

namespace BitcoinPaymentService.Data.Configuration
{
    public class TransactionConfiguration
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.MerchantOrderId).IsRequired();
            builder.Property(x => x.MerchantTimestamp).IsRequired();
            builder.Property(x => x.Status).HasConversion(new EnumToStringConverter<Status>()).IsRequired();
            builder.Property( x => x.UniqueHash).IsRequired();

            builder.Property(x => x.PaymentId).IsRequired();
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
