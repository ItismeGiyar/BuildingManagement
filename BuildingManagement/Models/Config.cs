using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Config
    {
        [Key]
        [StringLength(50)]
        [DisplayName("Key Code")]
        public string KeyCde {  get; set; } = string.Empty;
        [StringLength(100)]
        [DisplayName("Key Value")]
        public string KeyValue { get; set; } = string.Empty;


    }
}
