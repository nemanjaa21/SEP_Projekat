using AgencyService.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AgencyService.Data.Configuration
{
    public class PaymentServiceConfiguration : IEntityTypeConfiguration<PaymentService>
    {
        public void Configure(EntityTypeBuilder<PaymentService> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Subscribed);
        }
    }
}
