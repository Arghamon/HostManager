using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HostManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
    }
}
