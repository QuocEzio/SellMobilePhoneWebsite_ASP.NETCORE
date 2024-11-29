using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteBanDienThoai23.DAL.Models
{
    public partial class ChiTietGioHang
    {
        public int MaGh { get; set; }
        public string MaSp { get; set; }
        public int? SoLuong { get; set; }

        public virtual GioHang MaGhNavigation { get; set; }
        public virtual SanPham MaSpNavigation { get; set; }
    }
}
