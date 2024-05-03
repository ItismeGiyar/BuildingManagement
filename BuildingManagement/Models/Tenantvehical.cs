using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Tenantvehical
    {
        [Key]
        public int VehId { get; set; }
        public int TenantId { get; set; }
        [StringLength(20)]
        public required string PlateNo { get; set; }
        public int AllocateNo { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDteTime { get; set; }

    }
}
