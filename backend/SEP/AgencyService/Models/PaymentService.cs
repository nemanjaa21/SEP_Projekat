using Common.Models;
using System.ComponentModel.DataAnnotations;

namespace AgencyService.Models
{
    public class PaymentService : EntityBase
    {
        [Required]
        public string? Name { get; set; }
        public List<Agency>? Agencies { get; set; }
    }
}
