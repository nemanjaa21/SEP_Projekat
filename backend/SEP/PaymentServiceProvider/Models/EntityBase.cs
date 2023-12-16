using System.ComponentModel.DataAnnotations;

namespace PaymentServiceProvider.Models
{
    public class EntityBase
    {
        [Required]
        public int Id { get; set; }
    }
}
