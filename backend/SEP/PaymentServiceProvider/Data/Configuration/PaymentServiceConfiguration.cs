using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentServiceProvider.Models;

namespace PaymentServiceProvider.Data.Configuration
{
    public class PaymentServiceConfiguration : IEntityTypeConfiguration<PaymentService>
    {
        public void Configure(EntityTypeBuilder<PaymentService> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();

            builder.HasData(new PaymentService()
            {
                Id = 1,
                Name = "Credit Card Payment"
            },
            new PaymentService()
            {
                Id = 2,
                Name = "Bitcoin Payment"
            },
            new PaymentService()
            {
                Id = 3,
                Name = "QR Code Payment"
            },
            new PaymentService()
            {
                Id = 4,
                Name = "PayPal Payment"
            });
        }
    }
}
