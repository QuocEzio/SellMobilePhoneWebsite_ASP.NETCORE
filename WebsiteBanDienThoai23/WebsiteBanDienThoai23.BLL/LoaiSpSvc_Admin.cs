using System;
using System.Collections.Generic;
using System.Text;
using WebsiteBanDienThoai23.Common.BLL;
using WebsiteBanDienThoai23.Common.Req;
using WebsiteBanDienThoai23.Common.Rsp;
using WebsiteBanDienThoai23.DAL;
using WebsiteBanDienThoai23.DAL.Models;

namespace WebsiteBanDienThoai23.BLL
{
	public class LoaiSpSvc_Admin:GenericSvc<LoaiSpRep_Admin,LoaiSp>
	{
		private LoaiSpRep_Admin loaiSpRep_Admin;
		public LoaiSpSvc_Admin()
		{ 
			loaiSpRep_Admin = new LoaiSpRep_Admin();
		}
        public override SingleRsp Read(string key)
        {
            var res = new SingleRsp();
            var category = _rep.Read(key);
            if (category == null)
            {
                res.SetError("Không tìm thấy mã loại sản phẩm");
            }
            else
            {
                res.Data = category;
            }    
            return res;
        }
        public SingleRsp GetAllCategory()
        {
            var res = new SingleRsp();
            var category = _rep.GetAllCategory();
            res.Data = category;
            return res;
        }
        public SingleRsp CreateCategory(LoaiSpReq cateReq)
        {
            var res = new SingleRsp();
            var cate = new LoaiSp
            {
                MaLoai = cateReq.MaLoai,
                TenLoai=cateReq.TenLoai,
                Hinh= cateReq.Hinh
            };


            try
            {
                _rep.CreateCategory(cate);
                res.Data = cate;
                res.SetMessage("Đã thêm thành công loại sản phẩm!");
            }
            catch (Exception ex)
            {
                res.SetError($"Lỗi: {ex.Message}, Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
            }

            return res;
        }
        public SingleRsp UpdateCategory(LoaiSpReq cateReq)
        {
            var res = new SingleRsp();
            var cate = _rep.Read(cateReq.MaLoai);
            if (cate == null)
            {
                res.SetError("Không tìm thấy dữ liệu cho mã loại sản phẩm được chỉ định.");
            }
            else
            {
                cate.MaLoai = cateReq.MaLoai;
                cate.TenLoai = cateReq.TenLoai;
                cate.Hinh = cateReq.Hinh;
                try
                {
                    _rep.UpdateCategory(cate);
                    res.SetMessage("Cập nhật loại sản phẩm thành công");
                }
                catch (Exception ex)
                {
                    res.SetError($"Lỗi: {ex.Message}, Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
                }
            }
            return res;
        }
        public SingleRsp RemoveCategory(string key)
        {
            var res = new SingleRsp();
            var cate = _rep.Read(key);
            if (cate == null)
            {
                res.SetError("Không tìm thấy mã loại sản phẩm cần xóa");
            }
            else
            {
                try
                {
                    _rep.Remove(cate);
                    res.SetMessage("Xóa loại sản phẩm thành công.");
                }
                catch (Exception ex)
                {
                    res.SetError($"Lỗi: {ex.Message}, Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
                }
            }
            return res;
        }
		public SingleRsp ThongKeMM()
		{
			var res = new SingleRsp();
			var products = _rep.ThongKeMayMoi();
			res.Data = products;
			return res;
		}
		public SingleRsp ThongKeMC()
		{
			var res = new SingleRsp();
			var products = _rep.ThongKeMayCu();
			res.Data = products;
			return res;
		}
	}
}
