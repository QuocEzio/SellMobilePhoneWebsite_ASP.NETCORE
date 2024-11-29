using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteBanDienThoai23.BLL;
using WebsiteBanDienThoai23.Common.Req;
using WebsiteBanDienThoai23.Common.Rsp;

namespace WebsiteBanDienThoai23.AdminWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class SanPhamController : ControllerBase
	{
		private SanPhamSvc_Admin sanPhamSvc_Admin;
		
		public SanPhamController()
		{
			sanPhamSvc_Admin = new SanPhamSvc_Admin();
		}
		[HttpPost("Create-SanPham")]
		public IActionResult CreateSanPham([FromBody] SanPhamReq sanPhamReq)
		{
			var res = new SingleRsp();
			res = sanPhamSvc_Admin.CreateProduct(sanPhamReq);
			return Ok(res);
		}
		[HttpGet("PhanTrang-SanPham")]
        
        public IActionResult PhanTrangSanPham(int pageIndex, int pageSize)
        {
            var res = new SingleRsp();
                res = sanPhamSvc_Admin.PhanTrangSanPham(pageIndex, pageSize);
            return Ok(res);
        }
        [HttpGet("Get-All-SanPham")]
        public IActionResult GetAllSanPham()
        {
            var res = sanPhamSvc_Admin.GetAllProducts();
            return Ok(res);
        }

        [HttpGet("Get-SanPham-By-MaSp")]
        public IActionResult GetSanPhamByMaSp([FromQuery] string maSp)
        {
            var res = new SingleRsp();
            res = sanPhamSvc_Admin.GetSanPhamByMaSp(maSp);
            return Ok(res);
        }
        [HttpDelete("Remove-SanPham-By-MaSp")]
        public IActionResult RemoveSanPham([FromBody] SimpleReq simpleReq)
        {
			var res = new SingleRsp();
            res = sanPhamSvc_Admin.RemoveProduct(simpleReq.Keyword);
            return Ok(res);
        }

        [HttpPut("Update-SanPham-By-MaSp")]
        public IActionResult UpdateSanPham([FromBody] SanPhamReq sanPhamReq)
        {
            var res = new SingleRsp();
            res = sanPhamSvc_Admin.UpdateProduct(sanPhamReq);
            return Ok(res);
        }
    }
	
}
