using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostManager.Models
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int TermId { get; set; }

        [Required(ErrorMessage = "აუცილებელი ველი")]
        [Range(0, double.MaxValue, ErrorMessage = "მხოლოდ ციფრები")]
        public double PriceValue { get; set; }

        [ForeignKey("PackageId")]
        public Package Package { get; set; }

        [ForeignKey("TermId")]
        public Term Term { get; set; }

    }
}
