using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteBanDienThoai23.DAL.Models
{
    public partial class ChiTietHoaDon
    {
        public string MaSp { get; set; }
        public string MaHd { get; set; }
        public int? SoLuongDatHang { get; set; }
        public decimal? DonGia { get; set; }

        public virtual HoaDon MaHdNavigation { get; set; }
        public virtual SanPham MaSpNavigation { get; set; }
    }
}
