using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Menugp
    {
        [Key]
        public short MnugrpId { get; set; }
        public string MnugrpNme { get; set; } = string.Empty;
        public DateTime RevDteTime { get; set; }
    }
}
