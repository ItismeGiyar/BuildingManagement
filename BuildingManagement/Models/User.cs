using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(24)]
        public required string UserCde { get; set; }
        [StringLength(100)]
        public required string UserNme { get; set; }
        [StringLength(100)]
        public string Position { get; set; } = string.Empty;
        [StringLength(24)]
        public string Gender { get; set;} = string.Empty;
        public short MnugrpId { get; set; }
        public required byte[] Pwd { get; set; }
        public short CmpyId { get; set; }
        public DateTime RevDteTime { get; set; }

    }
}
