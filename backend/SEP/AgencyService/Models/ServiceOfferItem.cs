using AgencyService.Enums;

namespace AgencyService.Models
{
    public class ServiceOfferItem : EntityBase
    {
        public EOfferName OfferName { get; set; }
        public bool IsAccepted { get; set; }
        public double MonthlyPrice { get; set; }
        public double YearlyPrice { get; set; }
        public double SelectedPrice { get; set; }
        public List<ServiceOffer>? ServiceOffers { get; set; }
    }
}
