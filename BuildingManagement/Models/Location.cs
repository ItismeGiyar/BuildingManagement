using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class Location
    {
        [Key]
        [DisplayName("Location")]
        public int LocId { get; set; }
        
        [StringLength(50)]
        [DisplayName("Location Description")]
        public required string LocDesc { get; set; }
        [DisplayName("Company ID")]
        public short CmpyId { get; set; }
        [DisplayName("User ID")]
        public int UserId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevDteTime { get; set; }
        [NotMapped]
        [DisplayName("Company")]
        public string Company { get; internal set; }=string.Empty;
        [NotMapped]
        [DisplayName("User")]
        public string User { get; internal set; }=string.Empty;
    }
    
}
