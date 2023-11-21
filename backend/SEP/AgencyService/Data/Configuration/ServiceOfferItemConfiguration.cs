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
                MonthlyPrice = 24.99,
                YearlyPrice = 149.99
            },
            new ServiceOfferItem()
            {
                Id = 2,
                OfferName = EOfferName.ISSUANCE_OF_LAWS_ELECTRONIC_FORM,
                IsAccepted = false,
                MonthlyPrice = 11.99,
                YearlyPrice = 99.99
            },
            new ServiceOfferItem()
            {
                Id = 3,
                OfferName = EOfferName.ISSUANCE_OF_LAWS_PRINTED_FORM,
                IsAccepted = false,
                MonthlyPrice = 31.99,
                YearlyPrice = 240.00
            }
            , new ServiceOfferItem()
            {
                Id = 4,
                OfferName = EOfferName.PUBLICATION_OF_LAWS_ON_INTERNET,
                IsAccepted = false,
                MonthlyPrice = 6.99,
                YearlyPrice = 39.99
            });
        }
    }
}
