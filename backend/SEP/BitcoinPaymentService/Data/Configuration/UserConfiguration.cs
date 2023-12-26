using BitcoinPaymentService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BitcoinPaymentService.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x=> x.EthereumAddress);

            builder.HasData(new User()
            {
                Id = 3,
                Email = "malina@gmail.com",
            });
        }
    }
}
