using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace BuildingManagement.Models
{
    public class Company
    {
        [Key]
        public short CmpyId { get; set; }
        [StringLength(100)]
        public required string CmpyNme { get; set; } =String.Empty;
        public string? Address { get; set; }
        public DateTime RevDteTime { get; set; }
                
    }
}
