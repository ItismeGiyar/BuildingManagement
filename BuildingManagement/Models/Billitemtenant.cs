using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Billitemtenant
    {
        [Key]
        public int BtItemId {  get; set; }
        public int BItemId { get; set; }
        public int TenantId { get; set; }
        [StringLength(100)]
        public string SubPlan { get; set;}=string.Empty;
        public DateTime SubDte { get; set; }
        public Boolean ActiveFlg { get; set; }
        [StringLength(20)]
        public string? LastReadingUnit { get; set; }
        public decimal Amount { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDteTime { get; set; }

            
            
            
            
            
            
            
            
            
            
            }
}
