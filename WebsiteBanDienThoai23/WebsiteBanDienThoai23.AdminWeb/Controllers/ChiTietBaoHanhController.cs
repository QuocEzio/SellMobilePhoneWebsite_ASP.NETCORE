using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteBanDienThoai23.BLL;
using WebsiteBanDienThoai23.Common.Req;
using WebsiteBanDienThoai23.Common.Rsp;

namespace WebsiteBanDienThoai23.AdminWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietBaoHanhController : ControllerBase
    {
        private ChiTietBaoHanhSvc_Admin chiTietBaoHanhSpSvc_Admin;
        public ChiTietBaoHanhController()
        {
            chiTietBaoHanhSpSvc_Admin = new ChiTietBaoHanhSvc_Admin();
        }
        [HttpGet("Get-ChiTietBaoHanh-By-MaBH")]
        public IActionResult GetChiTietBaoHanhByMaLoai([FromQuery] int id)
        {
            var res = new SingleRsp();
            res = chiTietBaoHanhSpSvc_Admin.Read(id);
            return Ok(res);
        }
        [HttpGet("Get-All-ChiTietBaoHanh")]
        public IActionResult GetAllChiTietBaoHanh()
        {
            var res = new SingleRsp();
            res = chiTietBaoHanhSpSvc_Admin.GetAllGuarantee();
            return Ok(res);
        }
        [HttpPost("Create-ChiTietBaoHanh")]
        public IActionResult CreateChiTietBaoHanh([FromBody] ChiTietBaoHanhReq guaReq)
        {
            var res = new SingleRsp();
            res = chiTietBaoHanhSpSvc_Admin.CreateGuarantee(guaReq);
            return Ok(res);
        }
        [HttpPut("Update-ChiTietBaoHanh-By-MaBH")]
        public IActionResult UpdateChiTietBaoHanh([FromBody] ChiTietBaoHanhReq guaReq)
        {
            var res = new SingleRsp();
            res = chiTietBaoHanhSpSvc_Admin.UpdateGuarantee(guaReq);
            return Ok(res);
        }
        [HttpDelete("Remove-ChiTietBaoHanh-By-MaBH")]
        public IActionResult RemoveChiTietBaoHanh([FromBody] SimpleReq simpleReq)
        {
            var res = new SingleRsp();
            res = chiTietBaoHanhSpSvc_Admin.RemoveGuarantee(simpleReq.Id);
            return Ok(res);
        }
    }
}
