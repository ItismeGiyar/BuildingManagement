using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class Tenantvehical
    {
        [Key]
        
        public int VehId { get; set; }

        
        [DisplayName("Tenant")]

        public int TenantId { get; set; }
        [DisplayName("Plate Number")]
        [StringLength(20)]
       
        public required string PlateNo { get; set; }
        [DisplayName("Allocation Number")]
        public int AllocateNo { get; set; }
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevDteTime { get; set; }
        [NotMapped]
        [DisplayName("Company")]
        public string Company { get; set; } = string.Empty;

        [NotMapped]
        [DisplayName("User")]
        public string User { get; set; } = string.Empty;

    }
}
