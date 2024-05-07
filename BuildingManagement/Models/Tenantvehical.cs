using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Tenantvehical
    {
        [Key]
        
        public int VehId { get; set; }

        
        [DisplayName("Tenant Id")]

        public int TenantId { get; set; }
        [DisplayName("Plate Number")]
        [StringLength(20)]
       
        public required string PlateNo { get; set; }
        [DisplayName("Allocation Number")]
        public int AllocateNo { get; set; }
        [DisplayName("Company Id")]
        public short CmpyId { get; set; }
        [DisplayName("User Id")]
        public int UserId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevDteTime { get; set; }

    }
}
