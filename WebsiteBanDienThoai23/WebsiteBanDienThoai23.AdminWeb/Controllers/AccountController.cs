using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebsiteBanDienThoai23.DAL.Models;
using System.Linq;
using WebsiteBanDienThoai23.AdminWeb.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebsiteBanDienThoai23.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }

        private readonly QLBanDienThoaiContext _context;

        public AccountController(QLBanDienThoaiContext context)
        {
            _context = context;
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
            if (string.IsNullOrEmpty(model.MatKhau))
            {
                ModelState.AddModelError("MatKhau", "Vui lòng nhập mật khẩu.");
                return View(model);
            }
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var nd = _context.NguoiDungs.SingleOrDefault(kh => kh.TenTaiKhoan == model.TenTaiKhoan);
                if (nd != null && nd.IsAdmin == true)
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
                            return RedirectToAction("ListOrders", "BanHang");
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
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("DangNhap", "Account");
        }
    }
}
