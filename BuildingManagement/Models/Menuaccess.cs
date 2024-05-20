using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class Menuaccess
    {
        [Key]
        [DisplayName("Menu Access ID")]
        public int AccessId { get; set; }
        [DisplayName("Menu Group")]
        public short MnugrpId { get; set; }

        [StringLength(100)]
        [DisplayName("Button Name")]
        public string BtnNme { get; set; } = string.Empty;

        [DisplayName("Revised Date Time")]
        public DateTime RevDteTime { get; set; }

        [NotMapped]
        [DisplayName("Menu Group")]
        public string Menugp { get; set; } = string.Empty;
    }
}
