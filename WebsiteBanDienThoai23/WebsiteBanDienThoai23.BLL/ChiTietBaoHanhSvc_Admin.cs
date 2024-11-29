using System;
using System.Collections.Generic;
using System.Text;
using WebsiteBanDienThoai23.Common.BLL;
using WebsiteBanDienThoai23.Common.Req;
using WebsiteBanDienThoai23.Common.Rsp;
using WebsiteBanDienThoai23.DAL.Models;
using WebsiteBanDienThoai23.DAL;

namespace WebsiteBanDienThoai23.BLL
{
    public class ChiTietBaoHanhSvc_Admin : GenericSvc<ChiTietBaoHanhRep_Admin, ChiTietBaoHanh>
    {
        private ChiTietBaoHanhRep_Admin chiTietBaoHanhRep_Admin;
        public ChiTietBaoHanhSvc_Admin()
        {
            chiTietBaoHanhRep_Admin = new ChiTietBaoHanhRep_Admin();
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
        public SingleRsp CreateGuarantee(ChiTietBaoHanhReq guaReq)
        {
            var res = new SingleRsp();
            var gua = new ChiTietBaoHanh
            {
                MaBh = guaReq.MaBh,
                MaSp = guaReq.MaSp,
                NgayBatDau = guaReq.NgayBatDau,
                NgayKetThuc = guaReq.NgayKetThuc
            };
            try
            {
                _rep.CreateGuarantee(gua);
                res.Data = gua;
                res.SetMessage("Đã thêm thành công chi tiê bảo hành!");
            }
            catch (Exception ex)
            {
                res.SetError($"Lỗi: {ex.Message}, Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
            }

            return res;
        }
        public SingleRsp UpdateGuarantee(ChiTietBaoHanhReq guaReq)
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
                gua.MaSp = guaReq.MaSp;
                gua.NgayBatDau = guaReq.NgayBatDau;
                gua.NgayKetThuc = guaReq.NgayKetThuc;

                try
                {
                    _rep.UpdateGuarantee(gua);
                    res.SetMessage("Cập nhật chi tiết bảo hành thành công");
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
                    res.SetMessage("Xóa chi tiết bảo hành thành công.");
                }
                catch (Exception ex)
                {
                    res.SetError($"Lỗi: {ex.Message}, Inner Exception: {ex.InnerException?.Message}, StackTrace: {ex.StackTrace}");
                }
            }
            return res;
        }
        public static implicit operator ChiTietBaoHanhSvc_Admin(LoaiSpSvc_Admin v)
        {
            throw new NotImplementedException();
        }
    }
}
