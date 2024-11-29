using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using WebsiteBanDienThoai23.BLL;
using WebsiteBanDienThoai23.Common.Req;
using WebsiteBanDienThoai23.Common.Rsp;

namespace WebsiteBanDienThoai23.AdminWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoaiSpController : ControllerBase
	{
		private LoaiSpSvc_Admin loaiSpSvc_Admin;
		public LoaiSpController() 
		{
			loaiSpSvc_Admin = new LoaiSpSvc_Admin();
		}
        [HttpGet("Get-LoaiSP-By-MaLoaiSP")]
        public IActionResult GetLoaiSPByMaLoai([FromQuery] string key)
        {
            var res = new SingleRsp();
            res = loaiSpSvc_Admin.Read(key);
            return Ok(res);
        }
        [HttpGet("Get-All-LoaiSP")]
        public IActionResult GetAllLoaiSP()
        {
            var res = new SingleRsp();
            res = loaiSpSvc_Admin.GetAllCategory();
            return Ok(res);
        }
        [HttpPost("Create-LoaiSP")]
        public IActionResult CreateLoaiSP([FromBody] LoaiSpReq cateReq)
        {
            var res = new SingleRsp();
            res = loaiSpSvc_Admin.CreateCategory(cateReq);
            return Ok(res);
        }
        [HttpPut("Update-LoaiSP-By-MaLoaiSP")]
        public IActionResult UpdateCategory([FromBody] LoaiSpReq cateReq)
        {
            var res = new SingleRsp();
            res = loaiSpSvc_Admin.UpdateCategory(cateReq);
            return Ok(res);
        }
        [HttpDelete("Remove-LoaiSP-By-MaLoaiSP")]
        public IActionResult RemoveLoaiSP([FromBody] SimpleReq simpleReq)
        {
            var res = new SingleRsp();
            res = loaiSpSvc_Admin.RemoveCategory(simpleReq.Keyword);
            return Ok(res);
        }
		[HttpGet("ThongKe-SL-SpMoi-TheoLSP")]
		public IActionResult TKMayMoi()
		{
			var res = loaiSpSvc_Admin.ThongKeMM();
			return Ok(res);
		}
		[HttpGet("ThongKe-SL-SpCu-TheoLSP")]
		public IActionResult TKMayCu()
		{
			var res = loaiSpSvc_Admin.ThongKeMC();
			return Ok(res);
		}
	}
}
