using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebsiteBanDienThoai23.DAL.Models;
using WebsiteBanDienThoai23.Web.Models;
using System.Linq;
using WebsiteBanDienThoai23.Web.MailService;
using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using WebsiteBanDienThoai23.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace WebsiteBanDienThoai23.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }

        private readonly SendMailService _sendMailService;

        private readonly QLBanDienThoaiContext _context;

        public AccountController(QLBanDienThoaiContext context, SendMailService sendMailService)
        {
            _context = context;
            _sendMailService = sendMailService;
        }
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Dangky(DangKy model)
        {
            if (model.HoTen == null || model.Email == null || model.DiaChi == null || model.Sdt == null || model.TenTaiKhoan == null || model.MatKhau == null)
            {
                ViewBag.ThieuThongTin = "Vui lòng điều đầy đủ thông tin!";
                return View();
            }
            if (ModelState.IsValid)
            {
                var checkEmail = _context.NguoiDungs.FirstOrDefault(x => x.Email == model.Email);
                var checkTenTK = _context.NguoiDungs.FirstOrDefault(x => x.TenTaiKhoan == model.TenTaiKhoan);
                var checkSDT = _context.NguoiDungs.FirstOrDefault(x => x.Sdt == model.Sdt);
                if (checkEmail != null && checkTenTK == null && checkSDT == null)
                {
                    ViewBag.EmailError = "Email đã tồn tại!";
                    return View();
                }
                else if (checkEmail == null && checkTenTK != null && checkSDT == null)
                {
                    ViewBag.TenTKError = "Tên tài khoản đã tồn tại!";
                    return View();
                }
                else if (checkEmail == null && checkTenTK == null && checkSDT != null)
                {
                    ViewBag.SDTError = "Số điện thoại đã tồn tại!";
                    return View();
                }
                else if (checkEmail != null && checkTenTK != null && checkSDT == null)
                {
                    ViewBag.EmailError = "Email đã tồn tại!";
                    ViewBag.TenTKError = "Tên tài khoản đã tồn tại!";
                    return View();
                }
                else if (checkEmail == null && checkTenTK != null && checkSDT != null)
                {
                    ViewBag.SDTError = "Số điện thoại đã tồn tại!";
                    ViewBag.TenTKError = "Tên tài khoản đã tồn tại!";
                    return View();
                }
                else if (checkEmail != null && checkTenTK == null && checkSDT != null)
                {
                    ViewBag.SDTError = "Số điện thoại đã tồn tại!";
                    ViewBag.EmailError = "Email đã tồn tại!";
                    return View();
                }
                else if (checkEmail != null && checkTenTK != null && checkSDT != null)
                {
                    ViewBag.EmailError = "Email đã tồn tại!";
                    ViewBag.SDTError = "Số điện thoại đã tồn tại!";
                    ViewBag.TenTKError = "Tên tài khoản đã tồn tại!";
                    return View();
                }
                else if (string.IsNullOrEmpty(model.MatKhau))
                {
                    ModelState.AddModelError("MatKhau", "Vui lòng nhập mật khẩu.");
                    return View(model);
                }
                else
                {
                    model.IsAdmin = false;
                    NguoiDung nd = new NguoiDung();
                    nd.HoTen = model.HoTen;
                    nd.Email = model.Email;
                    nd.DiaChi = model.DiaChi;
                    nd.Sdt = model.Sdt;
                    nd.TenTaiKhoan = model.TenTaiKhoan;
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    string mk_hash = BCrypt.Net.BCrypt.HashPassword(model.MatKhau, salt);
                    nd.MatKhau = mk_hash;

                    nd.IsAdmin = false;

                    _context.Add(nd);
                    _context.SaveChanges();
                    return RedirectToAction("DangNhap", "Account");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(DangNhap model, string? ReturnUrl)
        {
            if (model.TenTaiKhoan == null || model.MatKhau == null)
            {
                ViewBag.ThieuThongTin = "Vui lòng điều đầy đủ thông tin!";
                return View();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var nd = _context.NguoiDungs.SingleOrDefault(kh => kh.TenTaiKhoan == model.TenTaiKhoan);
                if (nd != null && nd.IsAdmin == false)
                {
                    bool passwordMatch = BCrypt.Net.BCrypt.Verify(model.MatKhau, nd.MatKhau);
                    if (passwordMatch)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, nd.TenTaiKhoan),
                            new Claim("UserId", nd.UserId.ToString())
                        };
                        var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            new AuthenticationProperties
                            {
                                IsPersistent = true
                            });

                        HttpContext.Session.SetString("UserId", nd.UserId.ToString());
                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ViewBag.UserPassError = "Sai mật khẩu!";
                        return View();
                    }
                }
                else
                {

                    ViewBag.UserNameError = "Tên tài khoản chưa được đăng ký!";
                    return View();
                }
            }
            return RedirectToAction("DangNhap", "Account");
        }

        [HttpGet]
        public IActionResult QuenMatKhau()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> QuenMatKhau(string Email)
        {
            if (Email != null)
            {
                var nd = _context.NguoiDungs.SingleOrDefault(kh => kh.Email == Email);
                if (nd != null)
                {
                    try
                    {

                        Random rand = new Random();
                        string randomCode = (rand.Next(999999)).ToString();
                        TempData["RandomCode"] = randomCode;

                        MailContent content = new MailContent
                        {
                            To = nd.Email,
                            Subject = "Xác nhận tài khoản " + nd.TenTaiKhoan,
                            Body = String.Format("<div style=\"font-family: Helvetica,Arial,sans-serif;min-width:1000px;overflow:auto;line-height:2\">\n  <div style=\"margin:50px auto;width:70%;padding:20px 0\">\n    <div style=\"border-bottom:1px solid #eee\">\n      <a href=\"\" style=\"font-size:1.4em;color: #00466a;text-decoration:none;font-weight:600\">MoblilePhone Store</a>\n    </div>\n    <p style=\"font-size:1.1em\">Xin chào {0}</p>\n    <p>Chúng tôi đã nhận được yêu cầu đặt lại mật khẩu cho tài khoản của bạn. Nhập mã đặt lại mật khẩu sau đây:</p>\n    <h2 style=\"background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;\">{1}</h2>\n    <p style=\"font-size:0.9em;\">Trân trọng,<br />MoblilePhone Store</p>\n    <hr style=\"border:none;border-top:1px solid #eee\" />\n    <div style=\"float:right;padding:8px 0;color:#aaa;font-size:0.8em;line-height:1;font-weight:300\">\n      <p>MoblilePhone Store</p>\n      <p>97 Võ Văn Tần, Quận 3</p>\n      <p>TP HCM</p>\n    </div>\n  </div>\n</div>", nd.HoTen, randomCode)
                        };
                        int kq = await _sendMailService.SendMail(content);
                        if (kq == 1)
                        {
                            TempData["UserId"] = nd.UserId;
                            HttpContext.Session.SetInt32("UserId", nd.UserId);
                            return RedirectToAction("KhoiPhucMatKhau", "Account");
                        }
                        else
                        {
                            ViewBag.FailedRecovery = "Sai định dạng Email, nếu bạn đăng ký tài khoản bằng Email ảo, vui lòng liên hệ bộ phận CSKH";
                            return View();
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.FailedRecovery = "Sai định dạng Email, nếu bạn đăng ký tài khoản bằng Email ảo, vui lòng liên hệ bộ phận CSKH";
                        return View();
                    }
                }
                else
                {
                    ViewBag.FailedRecovery = "Email chưa được đăng ký tài khoản!";
                    return View();
                }
            }
            else
            {
                ViewBag.FailedRecovery = "Vui lòng điền đầy đủ thông tin!";
                return View();
            }
        }
        [HttpGet]
        public IActionResult KhoiPhucMatKhau()
        {
            return View();
        }
        [HttpPost]
        public IActionResult KhoiPhucMatKhau(string MaXacThuc, string MatKhau)
        {
            if (MaXacThuc == null || MatKhau == null)
            {
                ViewBag.ThieuThongTin = "Vui lòng điều đầy đủ thông tin!";
                return View();
            }

            string storedCode = TempData["RandomCode"] as string;
            int? userId = TempData["UserId"] as int?;
            var nd = _context.NguoiDungs.SingleOrDefault(kh => kh.UserId == userId);
            if (MaXacThuc == storedCode)
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string mk_hash = BCrypt.Net.BCrypt.HashPassword(MatKhau, salt);
                nd.MatKhau = mk_hash;
                _context.Update(nd);
                _context.SaveChanges();
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                ViewBag.CodeError = "Sai mã xác thực!";
                return View();
            }
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
        [HttpGet]
        public IActionResult DoiMatKhau()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult DoiMatKhau(string MatKhauCu, string MatKhauMoi)
        {
            if (MatKhauCu == null || MatKhauMoi == null)
            {
                ViewBag.ThieuThongTin = "Vui lòng điều đầy đủ thông tin!";
                return View();
            }

            if (ModelState.IsValid)
            {
                var userId = GetUserId();
                var nd = _context.NguoiDungs.FirstOrDefault(kh => kh.UserId == userId);

                if (nd != null)
                {
                    bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(MatKhauCu, nd.MatKhau);
                    if (isPasswordCorrect)
                    {
                        string salt = BCrypt.Net.BCrypt.GenerateSalt();
                        string mk_hash = BCrypt.Net.BCrypt.HashPassword(MatKhauMoi, salt);
                        nd.MatKhau = mk_hash;

                        _context.Update(nd);
                        _context.SaveChanges();

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.MatKhauCu= "Mật khẩu hiện tại không đúng!";
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            HttpContext.Session.Remove("CartModel");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]
        public IActionResult DoiThongTin()
        {
            var userId = GetUserId();
            var nd = _context.NguoiDungs.FirstOrDefault(kh => kh.UserId == userId);
            return View(nd);
        }
        [HttpPost]
        [Authorize]
		public IActionResult DoiThongTin(NguoiDung nd)
		{
			if (nd.HoTen == null || nd.Email == null || nd.DiaChi == null || nd.Sdt == null)
			{
				ViewBag.ThieuThongTin = "Vui lòng điều đầy đủ thông tin!";
				return View();
			}
			if (ModelState.IsValid)
			{
				var userId = GetUserId();
				List<NguoiDung> lnd = _context.NguoiDungs.ToList();
				var ndg = _context.NguoiDungs.FirstOrDefault(kh => kh.UserId == userId);
				lnd.Remove(ndg);
				var checkEmail = lnd.FirstOrDefault(x => x.Email == nd.Email);
				var checkSDT = lnd.FirstOrDefault(x => x.Sdt == nd.Sdt);
				if (checkEmail != null && checkSDT == null)
				{
					ViewBag.EmailError = "Email đã tồn tại!";
					return View();
				}
				else if (checkEmail == null && checkSDT != null)
				{
					ViewBag.SDTError = "Số điện thoại đã tồn tại!";
					return View();
				}
				else if (checkEmail != null && checkSDT != null)
				{
					ViewBag.EmailError = "Email đã tồn tại!";
					ViewBag.SDTError = "Số điện thoại đã tồn tại!";
					return View();
				}
				else
				{
					ndg.HoTen = nd.HoTen;
					ndg.Email = nd.Email;
					ndg.DiaChi = nd.DiaChi;
					ndg.Sdt = nd.Sdt;
					_context.Update(ndg);
					_context.SaveChanges();
					return RedirectToAction("Index", "Home");
				}
			}
			return View();
		}
	}
}