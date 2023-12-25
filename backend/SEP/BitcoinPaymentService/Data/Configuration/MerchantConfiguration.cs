using BitcoinPaymentService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BitcoinPaymentService.Data.Configuration
{
    public class MerchantConfiguration : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName);
            builder.Property(x => x.Merchant_Id).IsRequired();
            builder.Property(x => x.MerchantPassword).IsRequired();
        }
    }
}
