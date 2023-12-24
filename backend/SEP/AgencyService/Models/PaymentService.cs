using System.ComponentModel.DataAnnotations;

namespace AgencyService.Models
{
    public class PaymentService : EntityBase
    {
        [Required]
        public string? Name { get; set; }
        public bool Subscribed { get; set; }
        public Agency Agency { get; set; }
        public int AgencyId { get; set; }
    }
}
