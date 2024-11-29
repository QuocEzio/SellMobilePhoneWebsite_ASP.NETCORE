using System;
using System.Collections.Generic;
using WebsiteBanDienThoai23.DAL.Models;

namespace WebsiteBanDienThoai23.AdminWeb.Models
{
    public class BanHangModel
    {
        public string MaHd { get; set; }
        public string MaSp { get; set; }
        public string TenSp { get; set; }
        public int? SoLuongDatHang { get; set; }
        public decimal? DonGia { get; set; }

    }
}
