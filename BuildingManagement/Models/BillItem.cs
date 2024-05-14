using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class BillItem
    {
        [Key]
        public int BItemID { get; set; }
        [StringLength(50)]
        public string BItemDesc { get; set;}=string.Empty;
        public bool MonthPostFlg {  get; set; }
        public bool FixChrgFlg { get; set;}

         public decimal FixChrgAmt {  get; set; }
        public short CmpyId {  get; set; }
        public int UserId{ get; set;}
        public DateTime RevDteTime { get; set; }
    }
}
