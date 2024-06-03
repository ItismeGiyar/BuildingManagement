using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class PropertyInfo
    {
        [Key]
        [DisplayName("Property ID")]
        public int PropId { get; set; }
        [StringLength(100)]
        [DisplayName("Property Name")]
        public string PropNme { get; set; } = string.Empty;
        [StringLength(24)]
        [DisplayName("Phone 1")]
        public string Phone1 { get; set; } = string.Empty;
        [StringLength(24)]
        [DisplayName("Phone 2")]
        public string? Phone2 { get; set; }
        [StringLength(24)]
        [DisplayName("E mail")]
        public string? Email { get; set; }
        [StringLength(24)]
        [DisplayName("City")]
        public string City { get; set; } = string.Empty;
        [StringLength(24)]
        [DisplayName("Township")]
        public string Township { get; set; } = string.Empty;
        [DisplayName("Address")]
        public string Addr { get; set; } = string.Empty;
        [DisplayName("Acre Measure")]
        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative number.")]
        public decimal? AcreMeasure { get; set; }
        [DisplayName("Resident Type")]
        public int ResitypId { get; set; }
        [DisplayName("Block Count")]
        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative number.")]
        public short? BlockCount { get; set; }
        [DisplayName("Room Count")]
        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative number.")]
        public int? RoomCount { get; set; }
        [DisplayName("Parking Count")]
        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative number.")]
        public short? ParkingCount { get; set; }
        [DisplayName("Parking Size Description")]
        public string? ParkingSizeDesc { get; set; } = string.Empty;
        [DisplayName("Pool Count")]
        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative number.")]
        public short? PoolCount { get; set; }
        [DisplayName("Pool Size Desc")]
        public string? PoolSizeDesc { get; set; } = string.Empty;
        [DisplayName("Established Date")]
        public DateTime? EstiblishDte { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        [DisplayName("Revised Date Time")]
        public DateTime RevDteTime { get; set; }
        [NotMapped]
        [DisplayName("Resident Type")]
        public string Billitem { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("Company")]
        public string Company { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("User")]
        public string User { get; set; } = string.Empty;
        //public string? Tenant { get; internal set; }
    }
}
