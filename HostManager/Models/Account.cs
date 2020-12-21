using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostManager.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "მიუთითე დომენის სახელი")]
        public string DomainName { get; set; }

        [Required(ErrorMessage = "აირჩიე პაკეტი")]
        public int PackageId { get; set; }

        [Required(ErrorMessage = "აირჩიე ვადა")]
        public int TermId { get; set; }

        [DisplayName("SSL ჩართული")]
        public int SSLEnabled { get; set; } = 0;

        public double? SSLPrice { get; set; } = null;

        [Required(ErrorMessage = "არასწორი ფორმატი")]
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "არასწორი ფორმატი")]
        public DateTime PayDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "აირჩიე კომპანია")]
        public int CompanyId { get; set; }

        public bool Active { get; set; } = true;

        [ForeignKey("PackageId")]
        public Package Package { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [ForeignKey("TermId")]
        public Term Term { get; set; }
    }
}
