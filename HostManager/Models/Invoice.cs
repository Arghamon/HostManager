using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
