using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class BillItemTenant
    {
        [Key]
        [DisplayName("Tenant Subscribe ID")]
        public int BtitemId {  get; set; }
        [DisplayName("Subscribe Plan")]
        public int BItemId { get; set;}
        public int BItemID { get; internal set; }
        [DisplayName("Tenant")]

        public int TenantId {  get; set;}
        [StringLength(100)]
        public string SubPlan { get; set;}=string.Empty;
        [DisplayName("Subscribe Date")]
        public DateTime SubDte { get; set; }
        public bool ActiveFlg { get; set; }
        [StringLength(20)]
        [DisplayName("Last Reading Unit")]
        public string? LastReadingUnit {  get; set; }
        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative number.")]
        [DisplayName("Amount")]
        public decimal Amount {  get; set; }
        [DisplayName("Company")]
        public short CmpyId {  get; set; }
        [DisplayName("User")]
        public int UserId {  get; set; }
        [DisplayName("Revised Date Time")]
        public DateTime RevDteTime { get; set; }
        [NotMapped]
        [DisplayName("Tenant")]
        public string Tenant { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("Subscribe Plan")]
        public string Billitem { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("Company")]
        public string Company { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("User")]
        public string User { get; set; } = string.Empty;
    }
}
