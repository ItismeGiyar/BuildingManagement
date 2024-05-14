using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class Residenttype
    {
        [Key]
        public int ResitypId { get; set; }
        [StringLength(50)]
        [DisplayName("Resitdent Type Description")]
        public string RestypDesc { get; set; }=string.Empty;
        [DisplayName("Company ID")]
        public short CmpyId { get; set; }

        [DisplayName("User ID")]
        public int UserId {  get; set; }
        [DisplayName("Revised Date Time")]
        public DateTime RevdteTime { get; set; }
        [NotMapped]
        [DisplayName("Company")]
        public string Company { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("User")]
        public string User { get; set; } = string.Empty;

    }
}
