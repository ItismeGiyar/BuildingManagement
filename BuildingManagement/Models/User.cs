using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(24)]
        [DisplayName("User Code")]
        public required string UserCde { get; set; }
        [DisplayName("User Name")]
        [StringLength(100)]
        public required string UserNme { get; set; }
        [DisplayName("Position")]
        [StringLength(100)]
        public string Position { get; set; } = string.Empty;
        [DisplayName("Gender")]
        [StringLength(24)]
        public string Gender { get; set;} = string.Empty;
        [DisplayName("Menu Group")]
        public short MnugrpId { get; set; }
        [DisplayName("Password")]
        public required byte[] Pwd { get; set; }
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("Revised Datetime")]
        public DateTime RevDteTime { get; set; }
        [NotMapped]
        [DisplayName("Company")]
        public string Company { get; set; } = string.Empty;
    }
}
