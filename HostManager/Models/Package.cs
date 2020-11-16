using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HostManager.Models
{
    public class Package
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string Name { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        public string Description { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [Range(0, int.MaxValue, ErrorMessage = "მხოლოდ ციფრები")]
        public int Capacity { get; set; }

        public IList<Account> Accounts { get; set; }
    }
}
