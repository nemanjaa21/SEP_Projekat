using System.ComponentModel.DataAnnotations;

namespace PaymentServiceProvider.Models
{
    public class PaymentService
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
