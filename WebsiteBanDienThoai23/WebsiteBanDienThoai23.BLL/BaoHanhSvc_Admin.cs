using System;
using System.Collections.Generic;
using System.Text;
using WebsiteBanDienThoai23.Common.BLL;
using WebsiteBanDienThoai23.DAL.Models;
using WebsiteBanDienThoai23.DAL;
using WebsiteBanDienThoai23.Common.Rsp;
using WebsiteBanDienThoai23.Common.Req;

namespace WebsiteBanDienThoai23.BLL
{
    public class BaoHanhSvc_Admin : GenericSvc<BaoHanhRep_Admin, BaoHanh>
    {
        private BaoHanhRep_Admin baoHanhRep_Admin;
        public BaoHanhSvc_Admin()
        {
            baoHanhRep_Admin = new BaoHanhRep_Admin();
        }
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            var guarantee = _rep.Read(id);
            if (guarantee == null)
            {
                res.SetError("Không tìm thấy mã bảo hành sản phẩm");
            }
            else
            {
                res.Data = guarantee;
            }
            return res;
        }
        public SingleRsp GetAllGuarantee()
        {
            var res = new SingleRsp();
            var guarantee = _rep.GetAllGuarantee();
            res.Data = guarantee;
            return res;
        }
        public SingleRsp CreateGuarantee(BaoHanhReq guaReq)
        {
            var res = new SingleRsp();
            var gua = new BaoHanh
            {
                MaBh = guaReq.MaBh,
               MaKh = guaReq.MaKh,
            };
            try
            {
                _rep.CreateGuarantee(gua);
                res.Data = gua;
                res.SetMessage("Đã thêm thành công bảo hành!");
            }
            catch (Exception ex)
            {
                res.SetError($"Lỗi: {ex.Message}, Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
            }

            return res;
        }
        public SingleRsp UpdateGuarantee(BaoHanhReq guaReq)
        {
            var res = new SingleRsp();
            var gua = _rep.Read(guaReq.MaBh);
            if (gua == null)
            {
                res.SetError("Không tìm thấy dữ liệu cho mã bảo hành sản phẩm được chỉ định.");
            }
            else
            {
                gua.MaBh = guaReq.MaBh;
                gua.MaKh = guaReq.MaKh;

                try
                {
                    _rep.UpdateGuarantee(gua);
                    res.SetMessage("Cập nhật bảo hành thành công");
                }
                catch (Exception ex)
                {
                    res.SetError($"Lỗi: {ex.Message}, Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
                }
            }
            return res;
        }
        public SingleRsp RemoveGuarantee(int id)
        {
            var res = new SingleRsp();
            var gua = _rep.Read(id);
            if (gua == null)
            {
                res.SetError("Không tìm thấy mã bảo hành sản phẩm cần xóa");
            }
            else
            {
                try
                {
                    _rep.Remove(gua);
                    res.SetMessage("Xóa bảo hành thành công.");
                }
                catch (Exception ex)
                {
                    res.SetError($"Lỗi: {ex.Message}, Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
                }
            }
            return res;
        }
        public static implicit operator BaoHanhSvc_Admin(LoaiSpSvc_Admin v)
        {
            throw new NotImplementedException();
        }
    }
}
