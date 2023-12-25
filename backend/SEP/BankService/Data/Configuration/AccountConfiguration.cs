using BankService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankService.Data.Configuration
{
    public class AccountConifguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AccountNumber).IsRequired();
            builder.Property(x => x.Balance).IsRequired();
            builder.Property(x => x.Reserved).IsRequired();
            builder.Property(x => x.Merchant_Id);

            builder.HasOne(x => x.User)
                   .WithMany(u => u.Accounts)
                   .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Merchant)
                   .WithMany(a => a.Accounts)
                   .HasForeignKey(x => x.MerchantId);
        }
    }
}
