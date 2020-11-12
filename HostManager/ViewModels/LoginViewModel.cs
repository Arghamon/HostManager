using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HostManager.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "აუცილებელი ველი")]
        [EmailAddress(ErrorMessage = "ფორმატი არასწორია" )]
        public string Email { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [MinLength(6, ErrorMessage = "მინ 6 სიმბოლო")]
        public string Password { get; set; }
    }
}
