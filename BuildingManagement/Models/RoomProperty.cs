using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class RoomProperty
    {
        [Key]
        public int RoomId { get; set; }
        public required string RoomNo { get; set; }
        public int LocId { get; set; }
        public int BdTypId { get; set; }
        public float? SqFullMeasure { get; set; }
        public float? SqRoomeasure { get; set; }
        public string? AmenityDesc { get; set; }
        public string? FeatureDesc { get; set; }
        public string? Addr { get; set; }
        public int? TenantId { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDteTime { get; set; }
    }
}
