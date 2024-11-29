using System;
using System.Collections.Generic;
using System.Text;

namespace WebsiteBanDienThoai23.Common.Req
{
	public class SanPhamReq
	{
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
    }
}
