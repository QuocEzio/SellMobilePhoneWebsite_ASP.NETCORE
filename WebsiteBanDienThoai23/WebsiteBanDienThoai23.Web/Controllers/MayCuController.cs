using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteBanDienThoai23.DAL.Models;
using WebsiteBanDienThoai23.Web.Models;

namespace WebsiteBanDienThoai23.Web.Controllers
{
    public class MayCuController : Controller
    {
        private readonly QLBanDienThoaiContext _context = new QLBanDienThoaiContext();

        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 5;
            var lstSanPham = _context.SanPhams.AsQueryable();
            lstSanPham = lstSanPham.Where(n => n.MayCu == true);
            var totalItems = lstSanPham.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var itemsOnCurrentPage = lstSanPham.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var danhSachLoaiSP = await _context.LoaiSps.ToListAsync();

            var viewModel = new SanPhamModel
            {
                Items = itemsOnCurrentPage,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                DanhSachLoaiSP = danhSachLoaiSP
            };

            return View(viewModel);
        }


        public async Task<IActionResult> Name(string key)
        {
            var lstSanPham = _context.SanPhams.AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                lstSanPham = lstSanPham.Where(n => n.TenSp.Contains(key) && n.MayCu == true);
            }
            var totalItems = lstSanPham.Count();
            var itemsOnCurrentPage = lstSanPham.ToList();

            var danhSachLoaiSP = await _context.LoaiSps.ToListAsync();

            var viewModel = new SanPhamModel
            {
                Items = itemsOnCurrentPage,
                TotalItems = totalItems,
                DanhSachLoaiSP = danhSachLoaiSP
            };

            return View("Index", viewModel);
        }

        public async Task<IActionResult> Price(decimal min, decimal max)
        {
            var lstSanPham = _context.SanPhams.AsQueryable();
            lstSanPham = lstSanPham.Where(n => n.Gia * (1 - (n.GiamGia / 100)) >= min && n.Gia * (1 - (n.GiamGia / 100)) <= max && n.MayCu == true);
            var totalItems = lstSanPham.Count();
            var itemsOnCurrentPage = lstSanPham.ToList();

            var danhSachLoaiSP = await _context.LoaiSps.ToListAsync();

            var viewModel = new SanPhamModel
            {
                Items = itemsOnCurrentPage,
                TotalItems = totalItems,
                DanhSachLoaiSP = danhSachLoaiSP
            };

            return View("Index", viewModel);
        }

        public async Task<IActionResult> Filter(bool pintrau, bool cameradep, bool co5g)
        {
            var lstSanPham = _context.SanPhams.AsQueryable();
            lstSanPham = lstSanPham.Where(n => n.MayCu == true);
            if (pintrau)
            {
                lstSanPham = lstSanPham.Where(n => n.Pin > 3000);
            }

            if (cameradep)
            {
                lstSanPham = lstSanPham.Where(n => n.CameraSau > 12 && n.CameraTruoc > 12);
            }

            if (co5g)
            {
                lstSanPham = lstSanPham.Where(n => n._5g == 1);
            }

            var items = await lstSanPham.ToListAsync();
            var danhSachLoaiSP = await _context.LoaiSps.ToListAsync();

            var viewModel = new SanPhamModel
            {
                Items = items,
                TotalItems = items.Count,
                DanhSachLoaiSP = danhSachLoaiSP
            };

            return View("Index", viewModel);
        }

        public ActionResult Details(string id)
        {
            var product = _context.SanPhams.FirstOrDefault(p => p.MaSp == id);
            if (product == null)
            {
                return null;
            }

            var reviews = _context.DanhGia
                .Where(r => r.MaSp == id)
                .Join(_context.NguoiDungs,
                      r => r.MaKh,
                      u => u.UserId,
                      (r, u) => new DanhGiaModel
                      {
                          HoTen = u.HoTen,
                          SoSao = r.SoSao,
                          BinhLuan = r.BinhLuan
                      })
                .ToList();

            var viewModel = new SanPhamModel
            {
                ItemDetails = product,
                Reviews = reviews
            };

            return View(viewModel);
        }


        private int? GetUserId()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (int.TryParse(userIdString, out int userId))
            {
                return userId;
            }

            return null;
        }

        [HttpPost]
        [Authorize]
        public ActionResult SubmitReview(string MaSp, int SoSao, string BinhLuan)
        {
            int? userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }

            var existingReview = _context.DanhGia.FirstOrDefault(r => r.MaSp == MaSp && r.MaKh == userId.Value);

            if (existingReview != null)
            {
                existingReview.SoSao = SoSao;
                existingReview.BinhLuan = BinhLuan;
            }
            else
            {
                var review = new DanhGia
                {
                    MaSp = MaSp,
                    MaKh = userId.Value,
                    SoSao = SoSao,
                    BinhLuan = BinhLuan
                };
                _context.DanhGia.Add(review);
            }

            _context.SaveChanges();
            return RedirectToAction("Details", new { id = MaSp });
        }
    }
}
