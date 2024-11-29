using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebsiteBanDienThoai23.DAL.Models;

namespace WebsiteBanDienThoai23.AdminWeb.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly QLBanDienThoaiContext _context = new QLBanDienThoaiContext();
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(DateTime NgayBD, DateTime NgayKT)
        {
            if (NgayBD >= DateTime.Parse("1/1/2000") && NgayKT >= DateTime.Parse("1/1/2000"))
            {
				if (NgayBD <= NgayKT)
                {
					NgayKT = NgayKT.AddDays(1);
					ViewBag.SLDH = _context.HoaDons.Count(s => (s.NgayDatHang >= NgayBD) & (s.NgayDatHang <= NgayKT) & (s.TrangThaiTt == true));
                    ViewBag.DT = _context.HoaDons.Where(s => (s.NgayDatHang >= NgayBD) & (s.NgayDatHang <= NgayKT) & (s.TrangThaiTt == true)).Sum(a => a.TongGiaTri);
					ViewBag.DT = string.Format("{0:N0} VND", @ViewBag.DT);
					return View();
                }
                else
                {
                    ViewBag.NgayKT = "Ngày kết thúc không phù hợp";
                    return View();
                }
            }
            else
                return View();
		}
    }
}
