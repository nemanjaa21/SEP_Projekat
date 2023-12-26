using PayPalPaymentService.Enums;

namespace PayPalPaymentService.Dto
{
    public class ServiceOfferItemDto
    {
        public int Id { get; set; }
        public EOfferName OfferName { get; set; }
        public bool IsAccepted { get; set; }
        public double MonthlyPrice { get; set; }
        public double YearlyPrice { get; set; }
        public double SelectedPrice { get; set; }
    }
}
