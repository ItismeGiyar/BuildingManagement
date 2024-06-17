using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class BillPayment
    {
        [Key]
        [DisplayName("Bill Payment ID")]
        public int BillPId { get; set; }
        [DisplayName("Bill No")]
        public string BillNo { get; set; } = string.Empty;
        [StringLength(100)]
        [DisplayName("Bill Offset Description")]
        public string BillOffsetDesc { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative number.")]
        [DisplayName("Bill Amount ")]
        public decimal PayAmt { get; set;}
        [DisplayName("Current Code ")]
        public string CurrCde { get; set;}=string.Empty;
        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative number.")]
        [DisplayName("Current Rate")]
        public decimal CurrRate { get; set; }
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }
        [DisplayName("Revised Date Time")]
        public DateTime RevDteTime { get; set; }
        [NotMapped]
        [DisplayName("Company")]
        public string Company { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("User")]
        public string User { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("Bill No")]
        public string Bill { get; set; } = string.Empty;

    }
}
