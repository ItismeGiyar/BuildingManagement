using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class ComplaintCatg
    {
        [Key]
        [DisplayName("Complaint Category ID")]
        public int CmpCatgId { get; set; }
        [DisplayName("Complaint Category Code")]
        [StringLength(50)]
        public string CplCatCde { get; set; } = string.Empty;
        [DisplayName("Company Id")]
        public short CmpyId { get; set; }
        [DisplayName("User Id")]
        public int UserId { get; set; }
        [DisplayName("Revised Date Time")]
        public DateTime RevDteTime { get; set; }
        [NotMapped]
        [DisplayName("Company")]
        public string Company { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("User")]
        public string User { get; set; } = string.Empty;


    }
}
