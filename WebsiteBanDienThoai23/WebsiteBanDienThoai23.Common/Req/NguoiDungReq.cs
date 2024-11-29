using System;
using System.Collections.Generic;
using System.Text;

namespace WebsiteBanDienThoai23.Common.Req
{
    public class NguoiDungReq
    {
        public int UserId { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string Sdt { get; set; }
        public string TenTaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public bool IsAdmin { get; set; }
    }
}
