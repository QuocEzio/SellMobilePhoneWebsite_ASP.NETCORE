using System;

namespace WebsiteBanDienThoai23.Web.Models
{
    public class DonMuaModel
    {
        public string MaHd { get; set; }
        public string MaSp { get; set; }
        public string TenSp { get; set; }
        public int? SoLuongDatHang { get; set; }
        public decimal? DonGia { get; set; }
        public DateTime? NgayDatHang { get; set; }
        public decimal? TongGiaTri { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public bool? TrangThaiThanhToan { get; set; }
        public int? TrangThaiDonHang { get; set; }
        public DateTime? NgayNhanHang { get; set; }

    }
}
