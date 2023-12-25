using BankService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankService.Data.Configuration
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Pan).IsRequired();
            builder.Property(x => x.SecurityCode).IsRequired();
            builder.Property(x => x.CardHolderName).IsRequired();
            builder.Property(x => x.ExpirationDate).IsRequired();

            builder.HasOne(x => x.Account)
                   .WithMany(c => c.Cards)
                   .HasForeignKey(x => x.AccountId);
        }
    }
}
