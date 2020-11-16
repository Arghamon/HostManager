using System.ComponentModel.DataAnnotations;

namespace HostManager.Models
{
    public class Term
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [Range(6, int.MaxValue, ErrorMessage = "მხოლოდ ციფრები (მინ 6)")]
        public int Value { get; set; }
    }
}
