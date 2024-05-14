using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class TenantVehical
    {
        [Key]
        public int VehId {  get; set; }
        public int TenantId { get; set; }
        [StringLength(20)]
        public string PlateNo {  get; set; }=string.Empty;
        public int AllocateNo {  get; set; }
        public short CmpyId {  get; set; }
        public int UserId {  get; set; }
        public DateTime RevDteTime { get; set; }
            }
}
