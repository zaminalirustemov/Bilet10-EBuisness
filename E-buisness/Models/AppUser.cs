using Microsoft.AspNetCore.Identity;

namespace E_buisness.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
