using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, DataType(DataType.EmailAddress)]
        public override string Email { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }   
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
