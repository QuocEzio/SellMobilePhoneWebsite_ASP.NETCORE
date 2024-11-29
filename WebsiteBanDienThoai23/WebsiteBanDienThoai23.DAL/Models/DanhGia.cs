using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteBanDienThoai23.DAL.Models
{
    public partial class DanhGia
    {
        public string MaSp { get; set; }
        public int MaKh { get; set; }
        public int? SoSao { get; set; }
        public string BinhLuan { get; set; }

        public virtual NguoiDung MaKhNavigation { get; set; }
        public virtual SanPham MaSpNavigation { get; set; }
    }
}
