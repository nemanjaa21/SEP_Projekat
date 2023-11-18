using AgencyService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyService.Data.Configuration
{
    public class ServiceOfferConfiguration : IEntityTypeConfiguration<ServiceOffer>
    {
        public void Configure(EntityTypeBuilder<ServiceOffer> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
