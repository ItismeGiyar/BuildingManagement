using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Menuaccess
    {
        [Key]
        public int AccessId { get; set; }
        public short MnugrpId { get; set; }
        [StringLength(100)]
        public required string BtnNme { get; set; }
        public DateTime RevDteTime { get; set; }
    }
}
