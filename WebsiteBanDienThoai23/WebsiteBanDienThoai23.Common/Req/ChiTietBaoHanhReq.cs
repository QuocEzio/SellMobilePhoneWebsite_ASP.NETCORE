using System;
using System.Collections.Generic;
using System.Text;

namespace WebsiteBanDienThoai23.Common.Req
{
    public class ChiTietBaoHanhReq
    {
        public int MaBh { get; set; }
        public string MaSp { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
    }
}
