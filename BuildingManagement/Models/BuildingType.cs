using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class BuildingType
    {
        [Key]
        public int BdtypId { get; set; }
        [StringLength(50)]
        public string BdtypDesc { get; set; }=string.Empty;
        public short CmpyId { get; set; }
        public int UserId { get; set;}
        public DateTime RevDteTime { get; set;}

    }
}
