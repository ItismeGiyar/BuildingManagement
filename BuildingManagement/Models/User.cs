using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class User
    {
        [Key]

        public int UserId { get; set; }

        [StringLength(24)]
        [DisplayName("User Code")]
        public  string UserCde { get; set; } = string.Empty;

        [DisplayName("User Name")]
        [StringLength(100)]
        public string UserNme { get; set; } = string.Empty;
        [DisplayName("Position")]
        [StringLength(100)]
        public string Position { get; set; } = string.Empty;
        [DisplayName("Gender")]
        [StringLength(24)]
        public string Gender { get; set; } = string.Empty;
        [DisplayName("Menu Group")]
        public short MnugrpId { get; set; }
        
        [DisplayName("Password")]
        public  byte[]? Pwd { get; set; }
        
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("Revised Date Time")]
        public DateTime RevDteTime { get; set; }
       
        [NotMapped]
        [DisplayName("Company")]
        public string Company { get; set; } = string.Empty;

        [NotMapped]
        public string Password { get; set; } = string.Empty;
        
        [NotMapped]
        [DisplayName("Confirm Password")]
        public string ConfirmPwd { get; set; } = string.Empty;
        [NotMapped]
        
        [DisplayName("Menu Group Name")]
        public string MnuGrpNme { get; set; } = string.Empty;
        


    }
}
