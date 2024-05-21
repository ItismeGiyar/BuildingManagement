using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class Buildingtype
    {
        [Key]
        public int BdtypId { get; set; }
        [DisplayName("Building Type Description")]
        [StringLength(50)]
        public required string BdtypDesc { get; set; }
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
