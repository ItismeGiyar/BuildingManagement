using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Residenttype
    {
        [Key]
        public int ResitypId { get; set; }
        [StringLength(50)]
        public required string RestypDesc { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDteTime { get; set; }

    }
}
