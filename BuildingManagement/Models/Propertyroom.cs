using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class PropertyRoom
    {
        [Key]
        [DisplayName("Room Property ")]
        public int RoomId { get; set; }

        [DisplayName("Property ")]
        public int PropId { get; set; }
        [DisplayName("Room Number")]
        public string RoomNo { get; set; } = string.Empty;
        [DisplayName("Location")]
        public int LocId { get; set; }
        [DisplayName("Building Type")]
        public int BdtypId { get; set; }
        [DisplayName("Square Full Measure")]
        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative number.")]

        public decimal? SqFullMeasure { get; set; }
        [DisplayName("Square Room Measure")]
        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative number.")]
        public decimal? SqRooMeasure { get; set; }

        [DisplayName("Amenity Description ")]
        public string? AmenityDesc { get; set; }
        [DisplayName("Feature Description ")]

        public string? FeatureDesc { get; set; }
        [DisplayName("Address")]
        public string? Addr { get; set; }
        [DisplayName("Tenant")]
        public int? TenantId { get; set; }
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }
        [DisplayName("Revised Date Time")]
        public DateTime RevDteTime { get; set; }
        [NotMapped]
        [DisplayName("Property Info")]
        public string PropertyInfo { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("Tenant")]
        public string Tenant { get; internal set; } = string.Empty;
        [NotMapped]
        [DisplayName("Building Type")]
        public string BuildingType { get; internal set; } = string.Empty;
        [NotMapped]
        [DisplayName("Location")]
        public string Location { get; internal set; } = string.Empty;
        [NotMapped]
        [DisplayName("Company")]
        public string Company { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("User")]
        public string User { get; set; } = string.Empty;

    }
}
