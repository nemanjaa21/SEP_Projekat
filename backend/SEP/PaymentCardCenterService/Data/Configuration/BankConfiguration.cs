using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentCardCenterService.Models;

namespace PaymentCardCenterService.Data.Configuration
{
    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.HasKey(x => x.BankId);
            builder.Property(x => x.Url).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
