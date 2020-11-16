using System.ComponentModel.DataAnnotations;

namespace HostManager.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string Email { get; set; }
        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string Name { get; set; }
        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string Code { get; set; }
        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string ContactPerson { get; set; }
    }
}
