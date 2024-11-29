using System;
using System.Collections.Generic;

#nullable disable

namespace WebsiteBanDienThoai23.DAL.Models
{
    public partial class BaoHanh
    {
        public BaoHanh()
        {
            ChiTietBaoHanhs = new HashSet<ChiTietBaoHanh>();
        }

        public int MaBh { get; set; }
        public int? MaKh { get; set; }

        public virtual NguoiDung MaKhNavigation { get; set; }
        public virtual ICollection<ChiTietBaoHanh> ChiTietBaoHanhs { get; set; }
    }
}
