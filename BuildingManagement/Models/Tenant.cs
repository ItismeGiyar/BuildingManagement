using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class Tenant
    {
        

        [Key]
        [DisplayName("Tenant ID")]
        public int TenantId { get; set; }
        [StringLength(50)]
        [DisplayName("Tenant Name")]
        public string TenantNme { get; set; }=string.Empty;
        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative number.")]
        [DisplayName("Occupancy")]
        public short Occupany { get; set; }
        [StringLength(50)]

        public string IdNo { get; set; }=string.Empty;
        [StringLength(24)]
        public string Gender { get; set; }=string.Empty;
        [StringLength(50)]
        public string? Phone1 { get; set; }
        [StringLength(50)]
        public string? Phone2 { get; set; }
        public Boolean LocalFlg { get; set; }
        [DisplayName("Perment Address")]
        public string? PermentAddr { get; set; }
        [DisplayName("Company ID")]
        public short CmpyId { get; set; }
        [DisplayName("User ID")]
        public int UserId { get; set; }
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
