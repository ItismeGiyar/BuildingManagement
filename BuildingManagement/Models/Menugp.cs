using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Menugp
    {

        [Key]
        [DisplayName("Menu Group ID")]
        public short MnugrpId { get; set; }
        [DisplayName("Menu Group Name")]
        public string MnugrpNme { get; set; } = string.Empty;
        [DisplayName("Revised Date Time")]
        public DateTime RevDteTime { get; set; }

    }
}
