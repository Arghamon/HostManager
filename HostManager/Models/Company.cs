using System.ComponentModel.DataAnnotations;
using HostManager.Enums;

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

        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string Address { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [EnumDataType(typeof(InvoiceTemplate))]
        public InvoiceTemplate InvoiceTemplate { get; set; }
    }
}
