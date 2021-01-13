using System;
using System.ComponentModel.DataAnnotations;

namespace HostManager.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Path { get; set; }
    }
}
