using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAn_DineSmart.Models
{
    [Table("ThucDon")]
    public class ThucDon
    {
        [Key]
        public int MaMon { get; set; }
        public string TenMon { get; set; } = "";
        public string DanhMuc { get; set; } = "";
        public decimal GiaTien { get; set; }
        public bool TrangThai { get; set; } = true;
    }
}