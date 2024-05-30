using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class TenantVehical
    {
        [Key]
        [DisplayName("Tenant Vehicle ID")]
        public int VehId {  get; set; }
        [DisplayName("Tenant")]
        public int TenantId { get; set; }
        [StringLength(20)]
        [DisplayName("Plate No")]
        public string PlateNo {  get; set; }=string.Empty;
        [DisplayName("Allocate No")]
        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a non-negative integer.")]
        public int AllocateNo {  get; set; }
        [DisplayName("Company")]
        public short CmpyId {  get; set; }

        [DisplayName("User")]
        public int UserId {  get; set; }

        [DisplayName("Revised Date Time")]
        public DateTime RevDteTime { get; set; }
        [NotMapped]
        [DisplayName("Company")]
        public string Company { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("User")]
        public string User { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("Tenant")]
        public string Tenant { get; set; } = string.Empty;


    }
}
