using AgencyService.Models;

namespace AgencyService.DTO
{
    public class AgencyDto
    {
        public string Name { get; set; } = null!;
        public List<PaymentServiceDto>? PaymentServices { get; set; }
        public List<ServiceOfferItemDto>? ServiceOfferItems { get; set; }
    }
}
