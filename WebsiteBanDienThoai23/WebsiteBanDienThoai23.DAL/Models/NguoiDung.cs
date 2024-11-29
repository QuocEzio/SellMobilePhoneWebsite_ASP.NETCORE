using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteBanDienThoai23.DAL.Models
{
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            BaoHanhs = new HashSet<BaoHanh>();
            DanhGia = new HashSet<DanhGia>();
            GioHangs = new HashSet<GioHang>();
            HoaDonMaKhNavigations = new HashSet<HoaDon>();
            HoaDonMaNvNavigations = new HashSet<HoaDon>();
        }

        public int UserId { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string Sdt { get; set; }
        public string TenTaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public bool? IsAdmin { get; set; }

        public virtual ICollection<BaoHanh> BaoHanhs { get; set; }
        public virtual ICollection<DanhGia> DanhGia { get; set; }
        public virtual ICollection<GioHang> GioHangs { get; set; }
        public virtual ICollection<HoaDon> HoaDonMaKhNavigations { get; set; }
        public virtual ICollection<HoaDon> HoaDonMaNvNavigations { get; set; }
    }
}
