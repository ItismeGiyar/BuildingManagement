using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Complaintcatg
    {
        [Key]
        public int CplCatgId { get; set; }
        [StringLength(50)]
        [DisplayName("Complaint Category Code")]
        public string CplCatCde { get; set; } = string.Empty;
        public short CmpyId { get; set; }
        public int UserId { get; set; }
        public DateTime RevDteTime { get; set; }

    }
}
