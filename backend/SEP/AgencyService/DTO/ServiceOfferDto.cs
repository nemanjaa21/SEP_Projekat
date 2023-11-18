using AgencyService.Models;

namespace AgencyService.DTO
{
    public class ServiceOfferDto
    {
        public List<ServiceOfferItemDto>? ServiceOfferItems { get; set; }
        public int TotalPrice { get; set; }
    }
}
