using System.ComponentModel.DataAnnotations;

namespace BuildingManagement.Models
{
    public class PropertyInfo
    {
        [Key]
        public int PropId { get; set; }
        [StringLength(100)]
        public string PropNme { get; set; }=string.Empty;
        [StringLength(24)]
        public string Phone1 { get; set; }= string.Empty;   
        [StringLength(24)]
        public string Phone2 { get; set; }=string.Empty ;
        [StringLength(24)]
        public string Email { get; set; }=string.Empty;
        [StringLength(24)]
        public string City { get; set; }=string.Empty;
        [StringLength(24)]
        public string Township { get; set; }=string.Empty;
        public string Addr { get; set; }=string.Empty;
        public decimal AcreMeasure { get; set; }
        public int ResitypId{ get; set; }
        public short BlockCount { get; set; }
        public int RoomCount { get; set; }
        public short ParkingCount { get; set; }

        public string ParkingSizeDesc {  get; set; }=string.Empty;
        public short PoolCount { get; set; }
        public string PoolSizeDesc { get; set; } =string.Empty;
        public DateTime EstiblishDte { get; set; }
        public short CmpyId { get; set; }
        public int UserId { get; set;}
        public DateTime RevDteTime { get; set; }

    }
}
