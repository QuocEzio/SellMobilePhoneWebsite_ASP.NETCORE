using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteBanDienThoai23.BLL;
using WebsiteBanDienThoai23.Common.Req;
using WebsiteBanDienThoai23.Common.Rsp;

namespace WebsiteBanDienThoai23.AdminWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaoHanhController : ControllerBase
    {
        private BaoHanhSvc_Admin baoHanhSpSvc_Admin;
        public BaoHanhController()
        {
            baoHanhSpSvc_Admin = new BaoHanhSvc_Admin();
        }
        [HttpGet("Get-BaoHanh-By-MaBH")]
        public IActionResult GetBaoHanhByMaLoai([FromQuery] int id)
        {
            var res = new SingleRsp();
            res = baoHanhSpSvc_Admin.Read(id);
            return Ok(res);
        }
        [HttpGet("Get-All-BaoHanh")]
        public IActionResult GetAllBaoHanh()
        {
            var res = new SingleRsp();
            res = baoHanhSpSvc_Admin.GetAllGuarantee();
            return Ok(res);
        }
        [HttpPost("Create-BaoHanh")]
        public IActionResult CreateBaoHanh([FromBody] BaoHanhReq guaReq)
        {
            var res = new SingleRsp();
            res = baoHanhSpSvc_Admin.CreateGuarantee(guaReq);
            return Ok(res);
        }
        [HttpPut("Update-BaoHanh-By-MaBH")]
        public IActionResult UpdateBaoHanh([FromBody] BaoHanhReq guaReq)
        {
            var res = new SingleRsp();
            res = baoHanhSpSvc_Admin.UpdateGuarantee(guaReq);
            return Ok(res);
        }
        [HttpDelete("Remove-BaoHanh-By-MaBH")]
        public IActionResult RemoveBaoHanh([FromBody] SimpleReq simpleReq)
        {
            var res = new SingleRsp();
            res = baoHanhSpSvc_Admin.RemoveGuarantee(simpleReq.Id);
            return Ok(res);
        }
    }
}
