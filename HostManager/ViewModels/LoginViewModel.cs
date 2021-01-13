using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HostManager.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "აუცილებელი ველი")]
        [EmailAddress(ErrorMessage = "ფორმატი არასწორია")]
        public string Email { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [PasswordPropertyText]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public bool RememberMe { get; set; }
    }
}
