using System.ComponentModel.DataAnnotations;

namespace AgencyService.Models
{
    public class Agency : EntityBase
    {
        [Required]
        public string Name { get; set; } = null!;
        public List<PaymentService>? PaymentServices { get; set; }
        public List<ServiceOfferItem>? ServiceOfferItems { get; set;}
    }
}
