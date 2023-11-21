using AgencyService.Models;

namespace AgencyService.DTO
{
    public class ServiceOfferDto
    {
        public List<ServiceOfferItemDto>? ServiceOfferItems { get; set; }
        public double TotalPrice { get; set; }
        public int Id { get; set; }
    }
}
