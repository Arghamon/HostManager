using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HostManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string Lastname { get; set; }
    }
}
