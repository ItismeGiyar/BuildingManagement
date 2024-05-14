using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Location
    {
        [Key]
        public int LocId { get; set; }
        [StringLength(50)]
        public required string LocDesc { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDteTime { get; set; }

    }
    
}
