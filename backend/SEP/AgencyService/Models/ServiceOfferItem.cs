using AgencyService.Enums;

namespace AgencyService.Models
{
    public class ServiceOfferItem : EntityBase
    {
        public EOfferName OfferName { get; set; }
        public bool IsAccepted { get; set; }
        public int MonthlyPrice { get; set; }
        public int YearlyPrice { get; set; }
        public ServiceOffer ServiceOffer { get; set; }
        public int ServiceOfferId { get; set; }
    }
}
