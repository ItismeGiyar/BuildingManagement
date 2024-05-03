using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Buildingtype
    {
        [Key]
        public int BdtypId { get; set; }
        [StringLength(50)]
        public required string BdtypDesc { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDteTime { get; set; }

    }
}
