using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteBanDienThoai23.BLL;
using WebsiteBanDienThoai23.Common.Req;
using WebsiteBanDienThoai23.Common.Rsp;

namespace WebsiteBanDienThoai23.AdminWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private NguoiDungSvc_Admin nguoiDungSvc_Admin;
        

        public NguoiDungController()
        {
            nguoiDungSvc_Admin = new NguoiDungSvc_Admin();
        }
        [HttpPost("Create-NguoiDung")]
        public IActionResult CreateNguoiDung([FromBody] NguoiDungReq userReq)
        {
            var res = new SingleRsp();
            res = nguoiDungSvc_Admin.CreateUser(userReq);
            return Ok(res);
        }
        [HttpDelete("Remove-NguoiDung-By-UserID")]
        public IActionResult RemoveSanPham([FromBody] SimpleReq simpleReq)
        {
            var res = new SingleRsp();
            res = nguoiDungSvc_Admin.RemoveUser(simpleReq.Id);
            return Ok(res);
        }
        [HttpPut("Update-NguoiDung-By-UserID")]
        public IActionResult UpdateNguoiDung([FromBody] NguoiDungReq userReq)
        {
            var res = new SingleRsp();
            res = nguoiDungSvc_Admin.UpdateUser(userReq);
            return Ok(res);
        }
        [HttpGet("Get-All-NguoiDung")]
        public IActionResult GetAllNguoiDung()
        {
            var res = new SingleRsp();
            res = nguoiDungSvc_Admin.GetAllUsers();
            return Ok(res);
        }
        [HttpGet("Get-NguoiDung-By-UserID")]
        public IActionResult GetNguoiDungByUserID([FromQuery] int id)
        {
            var res = new SingleRsp();
            res = nguoiDungSvc_Admin.Read(id);
            return Ok(res);
        }
    }
}
