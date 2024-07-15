using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class Billledger
        
    {
        [Key]
        [DisplayName("Bill ID")]
        public int BillId { get; set; }
        [StringLength(20)]
        [DisplayName("Bill No")]
        public string BillNo { get; set;} = string.Empty;
        [DisplayName("Tran Date")]
        public DateTime TranDte { get; set; }
        [DisplayName("Tenant")]
        public int TenantId { get; set; }
        [DisplayName("Bill Item")]
        public int BItemID { get; set; }
        [StringLength(50)]
        [DisplayName("Bill Item Description")]
        public string BItemDesc { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative number.")]
        [DisplayName("Bill Amount")]
        public decimal BillAmt { get; set; }
       
            [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative number.")]
        [DisplayName("Paid Amount")]
        public decimal? PaidAmt { get; set; }
        [DisplayName("Pay Date")]
        public DateTime? PayDte { get; set; }
        [DisplayName("Generated Date")]
        public DateTime GeneratedDte { get; set; }
        [DisplayName("Remark")]
        public string? Remark { get; set; } 
        [DisplayName("Due Date")]
        public DateTime? DueDate { get; set; }
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }
        [DisplayName("Revised Date Time")]
        public DateTime RevDteTime { get; set; }
        [NotMapped]
        [DisplayName("Bill Item")]
        public string Billitem {  get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("Tenant")]
        public string Tenant { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("Company")]
        public string Company { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("User")]
        public string User { get; set; } = string.Empty;

    
    }
}
