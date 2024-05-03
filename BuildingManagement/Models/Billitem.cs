using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Billitem
    {
        [Key]
        public int BItemId { get; set; }
        [StringLength(50)]
        public required string BItemDesc { get; set; }
        public Boolean MonthPostFlg { get; set; }
        public Boolean FixChrgFlg { get; set; }
        public decimal FixChrgAmt { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDteTime { get; set; }

    }
}
