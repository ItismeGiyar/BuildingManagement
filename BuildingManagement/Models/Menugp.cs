using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class Menugp
    {
        
        [Key]
        public short MnugrpId { get; set; }
        [DisplayName("Menu Group Name")]
      
        public string MnugrpNme { get; set; }=string.Empty;

        public DateTime RevDteTime { get; set; }

    }
}
