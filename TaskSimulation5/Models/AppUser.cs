using Microsoft.AspNetCore.Identity;

namespace TaskSimulation5.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
