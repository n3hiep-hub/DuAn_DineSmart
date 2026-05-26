using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAn_DineSmart.Models
{
    [Table("DonHang")]
    public class DonHang
    {
        [Key]
        public int MaDonHang { get; set; }
        public int MaBan { get; set; }
        public int SoMon { get; set; }
        public string TrangThai { get; set; } = "";
        public decimal TongTien { get; set; }
        public DateTime ThoiGian { get; set; }

        [ForeignKey("MaBan")]
        public BanAn? Ban { get; set; }
    }
}