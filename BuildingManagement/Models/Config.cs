using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Config
    {
        [Key]
        [StringLength(50)]
        public required string KeyCde { get; set; }
        [StringLength(100)]
        public string KeyValue { get; set; } = string.Empty;


    }
}
