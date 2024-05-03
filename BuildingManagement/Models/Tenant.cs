using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Tenant
    {
        [Key]
        public int TenantId { get; set; }
        [StringLength(50)]
        public required string TenantNme { get; set; }
        public short Occupany { get; set; }
        [StringLength(50)]
        public  required string IdNo { get; set;}
        [StringLength(24)]
        public string? Gender { get; set; }
        [StringLength(50)]
        public string? Phone1 { get; set; }
        [StringLength(50)]
        public string? Phone2 { get; set; }
        public Boolean LocalFlg { get; set; }
        public String? PermentAddr { get; set; } = string.Empty;
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDteTime { get; set; }
    }
}
