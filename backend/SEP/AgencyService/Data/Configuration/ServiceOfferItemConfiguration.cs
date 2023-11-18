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
            builder.HasMany(x => x.ServiceOffers).WithMany(x => x.ServiceOfferItems);

            builder.HasData(new ServiceOfferItem()
            {
                Id = 1,
                OfferName = EOfferName.CODIFICATION_OF_LAWS,
                IsAccepted = false,
                MonthlyPrice = 250,
                YearlyPrice = 1250
            });
        }
    }
}
