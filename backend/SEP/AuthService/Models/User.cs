using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        public EUserType Type { get; set; }
    }
}
