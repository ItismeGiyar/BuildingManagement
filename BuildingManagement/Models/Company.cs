using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace BuildingManagement.Models
{
    public class Company
    {
        [Key]
        public short CmpyId { get; set; }
        [DisplayName("Company Name")]
        [StringLength(100)]
        public required string CmpyNme { get; set; } = String.Empty;
        [DisplayName("Address")]
        public string? Address { get; set; }
        [DisplayName("Revised Date Time")]
        public DateTime RevDteTime { get; set; }

    }
}
