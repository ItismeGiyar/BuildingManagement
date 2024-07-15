using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace BuildingManagement.Models
{
    public class Autonumber
    {
        [Key]
        [DisplayName("Auto No ID")]
        public int AutoNoId { get; set; }
        [DisplayName("Bill Prefix")]
        public string BillPrefix { get; set; } = string.Empty;
        [DisplayName("Business Date")]
        public DateTime BizDte { get; set; }
        [DisplayName("Zero Leading")]
        public Boolean ZeroLeading { get; set; }
        [DisplayName("Running No")]
        public short RunningNo {  get; set; }
        [DisplayName("Last Used No")]
        public long LastUsedNo { get; set; }
        [DisplayName("Last Generate Date")]
        public DateTime LastGenerateDte { get; set; }
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [NotMapped]
        [DisplayName("Company")]
        public string Company { get; set; } = string.Empty;

    }
}
