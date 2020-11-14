using System.ComponentModel.DataAnnotations;

namespace HostManager.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string ContactPerson { get; set; }
    }
}
