using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingManagement.Models
{
    public class Complainlog

    {
        [Key]
        [DisplayName("Complaint Log Name")]
        public int CmpId { get; set; }
        [DisplayName("Complaint Category ")]
        public int CmpCatgId { get; set; }
        [DisplayName("Tenant ")]
        public int TenantId { get; set; }
        [DisplayName("Complaint Date Time")]
        public DateTime CmpDteTime { get; set; }
        [DisplayName("Complaint Log Description")]
        public required string CmpDesc { get; set; }
        [DisplayName("Complaint Log Image")]
        public byte[]? CmpImg { get; set; }
        [NotMapped]
        [DisplayName("Complaint Log Image")]
        public IFormFile? CmpImgFile { get; set; }

        [NotMapped]
        public string? Base64Image { get; set; } = null;
        [DisplayName("Priority")]
        [StringLength(20)]
        public required string Priority { get; set; }
        [DisplayName("Resolved Description")]
        public string? ResolveDesc { get; set; }
        public bool ResolveFlg { get; set; }
        [DisplayName("Resolved By")]
        [StringLength(50)]
        public string? ResolveBy { get; set; }
        [DisplayName("Resolved Image")]
        public byte[]? ResolveImg { get; set; }
        [NotMapped]
        [DisplayName("Complaint Log Image")]
        public IFormFile? ResolveImgFile { get; set; }
        [DisplayName("Company")]
        public short CmpyId { get; set; }
        [DisplayName("User")]
        public int UserId { get; set; }
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
        [NotMapped]
        [DisplayName("Complaint Category")]
        public string ComplaintCatg { get; set; } = string.Empty;
        public string GetBase64Image()
        {
            if (CmpImg == null || CmpImg.Length == 0)
                return null;

            return Convert.ToBase64String(CmpImg);
        }

        public string GetBase64ResolveImage()
        {
            if (ResolveImg == null || ResolveImg.Length == 0)
                return null;

            return Convert.ToBase64String(ResolveImg);
        }
    }

}

