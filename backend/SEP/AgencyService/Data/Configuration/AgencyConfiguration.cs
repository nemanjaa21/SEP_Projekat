using AgencyService.Enums;
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

            builder.HasData(new Agency()
            {
                Id = 1,
                Name = "Agency",
                PaymentServices = new List<PaymentService>()
                {
                   new PaymentService()
                   {
                        Id = 1,
                        Name = "Credit Card Payment",
                   },
                   new PaymentService()
                   {
                        Id = 2,
                         Name = "Bitcoin Payment"
                   },
                    new PaymentService()
                    {
                        Id = 3,
                        Name = "QR Code Payment"
                    },
                    new PaymentService()
                    {
                        Id = 4,
                        Name = "PayPal Payment"
                    }
                },
                ServiceOfferItems = new List<ServiceOfferItem>
                {
                   new ServiceOfferItem()
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
                   }
                }
            });
        }
    }
}
