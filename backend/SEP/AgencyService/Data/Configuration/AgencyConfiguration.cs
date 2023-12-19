using AgencyService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyService.Data.Configuration
{
    public class AgencyConfiguration : IEntityTypeConfiguration<Agency>
    {
        public void Configure(EntityTypeBuilder<Agency> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany(x => x.PaymentServices).WithMany(x => x.Agencies);
            builder.HasMany(x=> x.Users).WithOne(x=> x.Agency).HasForeignKey(x=>x.AgencyId);
            builder.HasMany(x=> x.ServiceOfferItems).WithOne(x=> x.Agency).HasForeignKey(x=> x.AgencyId);
        }
    }
}
