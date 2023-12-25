using AgencyService.Enums;

namespace AgencyService.DTO
{
    public class CreateServiceOfferItemDto
    {
        public EOfferName OfferName { get; set; }
        public double MonthlyPrice { get; set; }
        public double YearlyPrice { get; set; }
    }
}
