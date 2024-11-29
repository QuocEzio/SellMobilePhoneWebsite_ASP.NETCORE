using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteBanDienThoai23.DAL.Models
{
    public partial class LoaiSp
    {
        public LoaiSp()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public string Hinh { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
