using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class ChangePwd
    {
        [Key]

        public int UserId { get; set; }

        [StringLength(24)]
        [DisplayName("User Code")]
        public string UserCde { get; set; } = string.Empty;

        [DisplayName("User Name")]
        [StringLength(100)]
        public string UserNme { get; set; } = string.Empty;

        [NotMapped]
        [DisplayName("Old Password")]
        public string OldPwd { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("New Password")]
        public string NewPwd { get; set; } = string.Empty;

        [NotMapped]
        public string Password { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("Confirm Password")]
        public string ConfirmPwd { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("Reset Password")]
        public string ResetPwd { get; set; } = string.Empty;

    }
}
