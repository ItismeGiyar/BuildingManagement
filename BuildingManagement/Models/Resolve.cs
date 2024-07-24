using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class Resolve

    {
        [Key]
        [DisplayName("Complaint Log Name")]
        public int CmpId { get; set; }
        [DisplayName("Complaint Category ")]
        
        public DateTime CmpDteTime { get; set; }
        [DisplayName("Complaint Log Description")]
        public string CmpDesc { get; set; } = string.Empty;
        
        [DisplayName("Priority")]
        [StringLength(20)]
        public string Priority { get; set; } = string.Empty;
        [DisplayName("Resolved Description")]
        public string? ResolveDesc { get; set; }
        public bool ResolveFlg { get; set; }
        [DisplayName("Resolved By")]
        [StringLength(50)]
        public string? ResolveBy { get; set; }
        [DisplayName("Resolved Image")]
        public byte[]? ResolveImg { get; set; }
        [NotMapped]
        [DisplayName("Resolved Image")]
        public IFormFile? ResolveImgFile { get; set; }
        [NotMapped]
        public string? Base64Image { get; set; } = null;
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }
        [DisplayName("Revised Date Time")]
        public DateTime RevDteTime { get; set; }
        public string GetBase64Image()
        {
            if (ResolveImg == null || ResolveImg.Length == 0)
                return null;

            return Convert.ToBase64String(ResolveImg);
        }
    }
}

