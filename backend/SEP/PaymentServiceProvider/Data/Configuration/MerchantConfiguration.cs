using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentServiceProvider.Models;

namespace PaymentServiceProvider.Data.Configuration
{
    public class MerchantConfiguration : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName);
            builder.Property(x => x.MerchantId).IsRequired();
            builder.Property(x => x.MerchantPassword).IsRequired();
            builder.Property(x => x.ApiKey);
        }
    }
}
