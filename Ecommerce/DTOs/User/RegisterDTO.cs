using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs.User
{
    public class RegisterDTO
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
