using AgencyService.Enums;
using AgencyService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AgencyService.Data.Configuration
{
    public class ServiceOfferItemConfiguration : IEntityTypeConfiguration<ServiceOfferItem>
    {
        public void Configure(EntityTypeBuilder<ServiceOfferItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.OfferName).HasConversion(new EnumToStringConverter<EOfferName>()).IsRequired().HasDefaultValue(EOfferName.CODIFICATION_OF_LAWS);
            builder.Property(x => x.IsAccepted);
            builder.Property(x=> x.MonthlyPrice).IsRequired();
            builder.Property(x=> x.YearlyPrice).IsRequired();
            builder.HasOne(x => x.ServiceOffer).WithMany(x => x.ServiceOfferItems)
                                                .HasForeignKey(x => x.ServiceOfferId)
                                                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
