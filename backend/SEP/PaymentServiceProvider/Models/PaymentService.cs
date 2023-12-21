using System.ComponentModel.DataAnnotations;

namespace PaymentServiceProvider.Models
{
    public class PaymentService : EntityBase
    {
        [Required]
        public string? Name { get; set; }
    }
}
