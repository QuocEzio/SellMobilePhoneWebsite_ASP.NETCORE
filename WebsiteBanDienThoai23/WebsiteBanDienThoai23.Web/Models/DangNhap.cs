using System.ComponentModel.DataAnnotations;

namespace WebsiteBanDienThoai23.Web.Models
{
    public class DangNhap
    {
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản của bạn!")]
        public string TenTaiKhoan { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu của bạn!")]
        public string MatKhau { get; set; }
    }
}
