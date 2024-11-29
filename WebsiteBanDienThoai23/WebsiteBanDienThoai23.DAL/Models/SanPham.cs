using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteBanDienThoai23.DAL.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            ChiTietBaoHanhs = new HashSet<ChiTietBaoHanh>();
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            DanhGia = new HashSet<DanhGia>();
        }

        public string MaSp { get; set; }
        public string TenSp { get; set; }
        public string Hinh { get; set; }
        public string MoTa { get; set; }
        public bool TonKho { get; set; }
        public short SoLuong { get; set; }
        public decimal Gia { get; set; }
        public byte? ThoiGianBh { get; set; }
        public int? _5g { get; set; }
        public string Cpu { get; set; }
        public short? Pin { get; set; }
        public short? SacNhanh { get; set; }
        public short? Ram { get; set; }
        public short? Rom { get; set; }
        public double? ManHinh { get; set; }
        public short? Fps { get; set; }
        public string HeDieuHanh { get; set; }
        public short? CameraSau { get; set; }
        public short? CameraTruoc { get; set; }
        public decimal GiamGia { get; set; }
        public string MaLoai { get; set; }
        public bool? MayCu { get; set; }

        public virtual LoaiSp MaLoaiNavigation { get; set; }
        public virtual ICollection<ChiTietBaoHanh> ChiTietBaoHanhs { get; set; }
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual ICollection<DanhGia> DanhGia { get; set; }
    }
}
