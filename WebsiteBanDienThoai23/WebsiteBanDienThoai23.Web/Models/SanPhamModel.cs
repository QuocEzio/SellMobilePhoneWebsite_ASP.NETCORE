using System.Collections.Generic;
using WebsiteBanDienThoai23.DAL.Models;

namespace WebsiteBanDienThoai23.Web.Models
{
    public class SanPhamModel
    {
        public List<SanPham> Items { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public List<LoaiSp> DanhSachLoaiSP { get; set; }

        public SanPham ItemDetails { get; set; }
        public List<DanhGiaModel> Reviews { get; set; }
    }
}
