
using Microsoft.AspNetCore.Identity;

namespace EventManager.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? LastOtpSentTime { get; set; }

    }
}
