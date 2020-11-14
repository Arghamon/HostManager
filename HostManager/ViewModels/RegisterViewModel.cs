using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostManager.ViewModels
{
    public class RegisterViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [EmailAddress(ErrorMessage = "ფორმატი არასწორია")]
        public string Email { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [MinLength(6, ErrorMessage = "მინ 6 სიმბოლო")]
        public string Password { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
