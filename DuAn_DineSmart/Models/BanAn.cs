using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAn_DineSmart.Models
{
    [Table("BanAn")]
    public class BanAn
    {
        [Key]
        public int MaBan { get; set; }
        public string TenBan { get; set; } = "";
        public string TrangThai { get; set; } = "Trống";
    }
}