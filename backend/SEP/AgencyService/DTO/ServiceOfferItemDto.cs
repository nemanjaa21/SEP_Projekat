using AgencyService.Enums;

namespace AgencyService.DTO
{
    public class ServiceOfferItemDto
    {
        public int Id { get; set; }
        public EOfferName OfferName { get; set; }
        public bool IsAccepted { get; set; }
        public int MonthlyPrice { get; set; }
        public int YearlyPrice { get; set; }
    }
}
