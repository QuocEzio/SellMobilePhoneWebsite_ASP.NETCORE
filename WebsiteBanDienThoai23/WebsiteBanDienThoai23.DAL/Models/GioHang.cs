﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteBanDienThoai23.DAL.Models
{
    public partial class GioHang
    {
        public GioHang()
        {
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
        }

        public int MaGh { get; set; }
        public int? MaKh { get; set; }

        public virtual NguoiDung MaKhNavigation { get; set; }
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }
    }
}