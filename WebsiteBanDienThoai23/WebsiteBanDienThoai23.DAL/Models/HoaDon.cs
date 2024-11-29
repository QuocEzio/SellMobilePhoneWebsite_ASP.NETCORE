using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteBanDienThoai23.DAL.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public string MaHd { get; set; }
        public int? MaKh { get; set; }
        public int? MaNv { get; set; }
        public DateTime? NgayDatHang { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public decimal? TongGiaTri { get; set; }
        public bool? TrangThaiTt { get; set; }
        public int? TrangThaiDh { get; set; }
        public DateTime? NgayNhanHang { get; set; }

        public virtual NguoiDung MaKhNavigation { get; set; }
        public virtual NguoiDung MaNvNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
