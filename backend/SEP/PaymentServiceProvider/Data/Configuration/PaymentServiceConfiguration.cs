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
                Id = 5,
                Name = "Credit_Card_Payment"
            },
            new PaymentService()
            {
                Id = 6,
                Name = "Bitcoin_Payment"
            },
            new PaymentService()
            {
                Id = 7,
                Name = "QR_Code_Payment"
            },
            new PaymentService()
            {
                Id = 8,
                Name = "PayPal_Payment"
            });
        }
    }
}
