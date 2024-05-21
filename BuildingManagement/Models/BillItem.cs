using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class BillItem
    {
        [Key]
        [DisplayName("Bill Item ID")]
        public int BItemID { get; set; }
        [StringLength(50)]
        [DisplayName("Bill Item Description")]
        public string BItemDesc { get; set;}=string.Empty;
        [DisplayName("Month Post Flag")]
        public bool MonthPostFlg {  get; set; }
        [DisplayName("Fix Charge Flag")]
        public bool FixChrgFlg { get; set;}
        [DisplayName("Fix Charge Amount")]

        public decimal FixChrgAmt {  get; set; }
        public short CmpyId {  get; set; }
        public int UserId{ get; set;}
        [DisplayName("Revised Date Time")]
        public DateTime RevDteTime { get; set; }
        [NotMapped]
        [DisplayName("Company")]
        public string Company { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("User")]
        public string User { get; set; } = string.Empty;
    }
}
