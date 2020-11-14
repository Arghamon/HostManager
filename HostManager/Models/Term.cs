using System.ComponentModel.DataAnnotations;

namespace HostManager.Models
{
    public class Term
    {
        [Key]
        public int Id { get; set; }
        public int Value { get; set; }
    }
}
