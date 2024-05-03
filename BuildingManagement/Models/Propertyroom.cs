using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Propertyroom
    {
        [Key]
        public int RoomId { get; set; }
        public int PropId { get; set; }
        [StringLength(50)]
        public required string RoomNo { get; set; }
        public int LocId { get; set; }
        public int BdtypId { get; set; }
        public decimal? SqFullMeasure { get; set; }
        public decimal? SqRooMeasure { get; set; }
        public string? AmenityDesc { get; set; } = string.Empty;
        public string? FeatureDesc { get; set; } = string.Empty;
        public string? Addr { get; set; } = string.Empty ;
        public int? TenantId { get; set; }
        public short CmpyId { get; set; }
        public int UserId{ get; set;}
        public DateTime RevDteTime { get; set; }
    }

}

