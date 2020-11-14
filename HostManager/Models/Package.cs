using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HostManager.Models
{
    public class Package
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Capacity { get; set; }

        public IList<Account> Accounts { get; set; }
    }
}
