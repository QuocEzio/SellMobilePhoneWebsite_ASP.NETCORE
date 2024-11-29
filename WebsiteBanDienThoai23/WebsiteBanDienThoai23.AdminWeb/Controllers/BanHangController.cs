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

namespace WebsiteBanDienThoai23.AdminWeb.Controllers
{
    public class BanHangController : Controller
    {
        private readonly QLBanDienThoaiContext _context = new QLBanDienThoaiContext();
        // GET: BanHang
        public async Task<IActionResult> ListOrders()
        {
            var  orders = _context.HoaDons
                .Include(h => h.MaKhNavigation)
                .Include(h => h.MaNvNavigation)
                .OrderByDescending(h => h.NgayDatHang);

            return View(await orders.ToListAsync());
        }

        public async Task<IActionResult> OrderDetails(string MaHd)
        {
            var orderDetails = await _context.ChiTietHoaDons
                .Include(c => c.MaSpNavigation)
                .Where(c => c.MaHd == MaHd) 
                .OrderByDescending(c => c.MaHd) 
                .ToListAsync();

            return View(orderDetails);
        }

        // GET: BanHang/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.MaKhNavigation)
                .Include(h => h.MaNvNavigation)
                .FirstOrDefaultAsync(m => m.MaHd == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // GET: BanHang/Create
        public IActionResult Create()
        {
            ViewData["MaKh"] = new SelectList(_context.NguoiDungs, "UserId", "UserId");
            ViewData["MaNv"] = new SelectList(_context.NguoiDungs, "UserId", "UserId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHd,MaKh,MaNv,NgayDatHang,DiaChiGiaoHang,TongGiaTri,TrangThaiTt,TrangThaiDh,NgayNhanHang")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListOrders));
            }
            ViewData["MaKh"] = new SelectList(_context.NguoiDungs, "UserId", "UserId", hoaDon.MaKh);
            ViewData["MaNv"] = new SelectList(_context.NguoiDungs, "UserId", "UserId", hoaDon.MaNv);
            return View(hoaDon);
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
        // GET: BanHang/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            var NhanVienLapDon = await _context.NguoiDungs.FindAsync(GetUserId());
            ViewBag.MaNhanVienLapDon = NhanVienLapDon.UserId;
            hoaDon.MaNv = ViewBag.MaNhanVienLapDon;

            ViewData["TrangThaiTt"] = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Chưa thanh toán", Value = "false", Selected = hoaDon.TrangThaiTt == false },
                new SelectListItem { Text = "Đã thanh toán", Value = "true", Selected = hoaDon.TrangThaiTt == true },
            }, "Value", "Text");

            ViewData["TrangThaiDh"] = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Đang xử lý", Value = "0", Selected = hoaDon.TrangThaiDh == 0 },
                new SelectListItem { Text = "Đang giao hàng", Value = "1", Selected = hoaDon.TrangThaiDh == 1 },
                new SelectListItem { Text = "Đã giao hàng", Value = "2", Selected = hoaDon.TrangThaiDh == 2 }
            }, "Value", "Text");

            return View(hoaDon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaHd,MaKh,MaNv,NgayDatHang,DiaChiGiaoHang,TongGiaTri,TrangThaiTt,TrangThaiDh,NgayNhanHang")] HoaDon hoaDon)
        {
            if (id != hoaDon.MaHd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();

                    if (hoaDon.TrangThaiTt == true && hoaDon.TrangThaiDh == 2)
                    {
                        var baoHanh = new BaoHanh
                        {
                            MaKh = hoaDon.MaKh,
                        };
                        _context.BaoHanhs.Add(baoHanh);
                        await _context.SaveChangesAsync();

                        var chiTietHoaDons = await _context.ChiTietHoaDons
                            .Where(c => c.MaHd == hoaDon.MaHd)
                            .ToListAsync();

                        foreach (var chiTiet in chiTietHoaDons)
                        {
                            var sanPham = await _context.SanPhams.FindAsync(chiTiet.MaSp);
                            if (sanPham != null)
                            {
                                var chiTietBaoHanh = new ChiTietBaoHanh
                                {
                                    MaBh = baoHanh.MaBh,
                                    MaSp = chiTiet.MaSp,
                                    NgayBatDau = hoaDon.NgayNhanHang,
                                    NgayKetThuc = hoaDon.NgayNhanHang.Value.AddYears((int)sanPham.ThoiGianBh) 
                                };
                                _context.ChiTietBaoHanhs.Add(chiTietBaoHanh);
                            }
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(hoaDon.MaHd))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListOrders));
            }
            ViewData["MaKh"] = new SelectList(_context.NguoiDungs, "UserId", "UserId", hoaDon.MaKh);
            ViewData["MaNv"] = new SelectList(_context.NguoiDungs, "UserId", "UserId", hoaDon.MaNv);
            return View(hoaDon);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.MaKhNavigation)
                .Include(h => h.MaNvNavigation)
                .FirstOrDefaultAsync(m => m.MaHd == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hoaDon = await _context.HoaDons.Include(h => h.ChiTietHoaDons).FirstOrDefaultAsync(h => h.MaHd == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            _context.ChiTietHoaDons.RemoveRange(hoaDon.ChiTietHoaDons);
            _context.HoaDons.Remove(hoaDon);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListOrders));
        }


        private bool HoaDonExists(string id)
        {
            return _context.HoaDons.Any(e => e.MaHd == id);
        }
    }
}
