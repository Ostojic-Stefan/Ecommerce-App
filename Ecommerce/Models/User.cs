using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
