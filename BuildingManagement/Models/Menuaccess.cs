using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class Menuaccess
    {
        [Key]
        public int AccessId { get; set; }
        [DisplayName("Menu Group ID")]
        public short  MnugrpId { get; set; }
        [DisplayName("Menu Group Name")]
        [StringLength(100)]
        public string BtnNme { get; set; } = string.Empty;

        [DisplayName("Revised Datetime")]
        public DateTime RevDteTime { get; set; }

        [NotMapped]
        [DisplayName("Menu Group")]
        public string Menugp { get; set; } = string.Empty;
    }
}
